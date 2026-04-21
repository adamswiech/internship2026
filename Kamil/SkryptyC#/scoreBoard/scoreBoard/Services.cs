using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using scoreBoard.Models;

namespace scoreBoard
{
    public class Services
    {
        private readonly AppDbContext _context;
        public Services(AppDbContext context)
        {
            _context = context;
        }

        public void PrintMessages()
        {
            var count = _context.Score.Count();
            Console.WriteLine($"Current score count: {count}");
        }
        public void ProcessScoresJob(ScoreDTO score)
        {
            _context.Score.Add(new Score
            {
                Points = score.Points,
                PlayerName = score.PlayerName,
                Status = (score.Status != null) ? score.Status : "in progers"
            });

            _context.SaveChanges();
        }
        public void RecalculateLeaderboard()
        {
            var score = _context.Score.OrderBy(s => s.Points).Reverse().Take(10);

            _context.Database.ExecuteSqlRaw("truncate table Leaderboard");

            _context.Leaderboard.AddRange(score.Select(s => new Leaderboard
            {
                Score = s
            }));
            _context.SaveChanges();
        }
        [DisableConcurrentExecution(60)]
        public void CheckForCheaters()
        {
            var avg = _context.Score.Average(s => s.Points);
            _context.Score.ExecuteUpdate(setters => setters.SetProperty(s => s.Status, s => "verify"));
            _context.Score.Where(s => s.Points > 10 * avg).ExecuteUpdate(setters => setters.SetProperty(s => s.Status, s => "sus"));


            _context.SaveChanges();
        }
        [DisableConcurrentExecution(60)]
        public void SaveLeaderBoard()
        {
            var newScores = _context.Leaderboard
                .Where(s => !_context.Historic.Any(h => h.Score.Id == s.Score.Id));
            _context.Historic.AddRange(newScores.Select(s => new Historic
            {
                Score = s.Score
            }));
            _context.SaveChanges();
        }
        public void Add1000Records()
        {
            for (int i = 0; i < 1000; i++)
            {
                var randomScore = new ScoreDTO
                {
                    Points = (int)new Random().NextInt64(0, 1000),
                    PlayerName = $"Player{new Random().Next(1, 100)}",
                };
                this.ProcessScoresJob(randomScore);

            }
        }
        [AutomaticRetry(Attempts = 3)]
        public void FailJob()
        {
            Console.WriteLine("This job will fail and retry 3 times");
            throw new Exception("This job is meant to fail");
        }
    }
}
