using Hangfire;
using LeaderBoardApp.Server.Data;
using LeaderBoardApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaderBoardApp.Server.Services
{
    public class LeaderBoardService
    {
        private readonly AppDbContext _context;
        private readonly IBackgroundJobClient _jobClient;
        public static List<Player> CachedLeaderBoard { get; private set; } = new();
        private int GetAvgScore()
        {
            if (!_context.PlayersSet.Any()) return 0;
            return (int)_context.PlayersSet.Average(p => p.score);
        }

        public LeaderBoardService(AppDbContext context, IBackgroundJobClient jobClient)
        {
            _context = context;
            _jobClient = jobClient;
        }

        public async Task ProcessScore(Player player)
        {
            var playerExists = _context.PlayersSet.FirstOrDefault(p => p.playerId == player.playerId);
            var currentScore = 0; //if player exists - this is score of player

            if (playerExists != null)
            {
                currentScore = playerExists.score;
            }

            var avgScore = 10 * GetAvgScore();
            if ((player.score > avgScore && avgScore != 0))
            {
                player.status = "suspicious";
            }
            else if (currentScore != 0)
            {
                if ((player.score > 5 * currentScore))
                {
                    player.status = "suspicious";
                }
            }
            else
            {
                player.status = "verified";
            }

            if (playerExists == null)
            {
                _context.PlayersSet.Add(player);
            }
            else
            {
                playerExists.score = player.score;
                playerExists.status = player.status;
                playerExists.gameMode = player.gameMode;

                _context.PlayersSet.Update(playerExists);
            }

            _context.SaveChanges();
        }
        public void QueuePlayerScore(Player player)
        {
            if (player.score <= 0)
                throw new ArgumentException("Score wasn't set properly.");

            BackgroundJob.Enqueue(() => ProcessScore(player));
        }

        public void RecalculateLeaderboard(string gameMode = "")
        {
            CachedLeaderBoard = _context.PlayersSet
        .AsNoTracking()
        .Where(p => string.IsNullOrEmpty(gameMode) || p.gameMode == gameMode)
        .OrderByDescending(p => p.score)
        .Take(10)
        .ToList();
        }
    }
}