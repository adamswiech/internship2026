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

        public async Task ProcessScore(Player player)
        {
            //async function that processes score of player

            if (player.score > 1200) //change it to avg value 
            {
                player.status = "suspicious";
            }
            else
            {
                player.status = "verified";
            }


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