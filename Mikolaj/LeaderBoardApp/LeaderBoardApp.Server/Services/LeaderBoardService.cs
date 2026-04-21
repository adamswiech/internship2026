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

        public LeaderBoardService(AppDbContext context, IBackgroundJobClient jobClient)
        {
            _context = context;
            _jobClient = jobClient;
        }

        public void ProcessScore(Player player)
        {
            _context.PlayersSet.Add(player);
            _context.SaveChanges();
        }
        public void QueuePlayerScore(Player player)
        {
            BackgroundJob.Enqueue(() => ProcessScore(player));
        }

        public IEnumerable<Player> FetchScores()
        {
            return _context.PlayersSet.AsNoTracking().ToList();
        }
    }
}