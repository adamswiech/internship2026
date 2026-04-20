using Hangfire;
using leaderboardServer.Data;
using leaderboardServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata.Ecma335;

namespace leaderboardServer.Services
{

    public class leaderboardService
    {
        private readonly dbContext _context;
        private readonly IMemoryCache _cache;

        public leaderboardService(dbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [Queue("critical")]
        public async Task processScore(Score score)
        {
            if (score.score <= 0)
                throw new ArgumentException("Score must be positive");


            var avg = await GetAvgScore();
            var top = await GetPlayerBest(score.username);
            
            score.isSuspicious = 
                score.score > 5 * top ||
                score.score > 10 * avg ||
                (top > 0 && score.score > top * 5);
            
            await addScore(score);
        }



        public async Task addScore(Score score)
        {
            _context.Scores.Add(score);
            await _context.SaveChangesAsync();
        }




        public async Task<List<Top10>> getTop10()
        {
            var res = await _context.Top10.AsQueryable().AsNoTracking().Include(x => x.Score).ToListAsync();
            return res;
        }



        [Queue("default")]
        public async Task simulate()
        {
            for (int i = 0; i < 1000; i++)
            {
                var score = new Score
                {
                    username = $"player_{i}",
                    score = Random.Shared.Next(100, 10000),
                    gameMode = "solo",
                    time = TimeSpan.TryParse("10:10:11", out var result) ? result : TimeSpan.Zero
                };
                BackgroundJob.Enqueue(() => processScore(score));
            }
        }


        
        public async Task<List<Top10snapshot>> getSnapshots()
        {
            return await _context.Top10snapshots
                .AsNoTracking()
                .AsQueryable()
                .Include(x => x.Entries)
                .ToListAsync();
        }





        public async Task<double> GetAvgScore()
        {
            return await _cache.GetOrCreateAsync("avgScore", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);

                var hasScores = await _context.Scores.AnyAsync();
                if (!hasScores)
                    return 0.0;

                return await _context.Scores.AverageAsync(x => x.score);
            });
        }




        [Queue("default")]
        [DisableConcurrentExecution(30)]
        public async Task genTop10()
        {
            try
            {
            var scores = await _context.Scores
                .AsNoTracking()
                .OrderByDescending(x => x.score)
                .Take(10)
                .ToListAsync();

            _context.Top10.RemoveRange(_context.Top10);

            var top10 = scores.Select((s, index) => new Top10
            {
                rank = index + 1,
                scoreId = s.id
            }).ToList();

            await _context.Top10.AddRangeAsync(top10);
            await _context.SaveChangesAsync();
        }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"genTop10 failed: {ex.Message}");
                throw;
            }
        }
        [Queue("default")]
        public async Task snapshotTop10()
        {
            try
            {
            var top10 = await _context.Top10
                .AsNoTracking()
                .OrderByDescending(x => x.rank)
                .Include(x => x.Score)
                .ToListAsync();

            var snapshot = new Top10snapshot
            {
                date = DateTime.UtcNow,
                Entries = top10.Select(x => new snapshotEntry
                {
                    rank = x.rank ?? 0,
                    username = x.Score.username,
                    score = x.Score.score,
                    time = x.Score.time,
                    gameMode = x.Score.gameMode,
                    isSuspicious = x.Score.isSuspicious ?? false
                }).ToList()
            };

            _context.Top10snapshots.Add(snapshot);
            await _context.SaveChangesAsync();
        }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"snapshotTop10 failed: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetPlayerBest(string username)
        {
            return await _context.Scores
                .Where(x => x.username == username)
                .OrderByDescending(x => x.score)
                .Select(x => x.score)
                .FirstOrDefaultAsync();
        }
    }
}
