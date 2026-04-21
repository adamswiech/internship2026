using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using scoreBoard.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace scoreBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly AppDbContext _context;
        private Services services;
        private ScoreJobManager scoreJobManager;
        public ScoreController(AppDbContext context)
        {
            _context = context;
            services = new Services(context);
            scoreJobManager = new ScoreJobManager();
        }
        [HttpGet]
        public ActionResult<List<Score>> Get()
        {
            return _context.Leaderboard.Select(s => s.Score).ToList();
        }
        [HttpGet("GetHistoricScores")]
        public ActionResult<List<ScoreDTO>> GetHistoric()
        {
            return _context.Historic.Select(h => new ScoreDTO
            {
                PlayerName = h.Score.PlayerName,
                Points = h.Score.Points,
                Status = h.Score.Status
            }).ToList();
        }

        [HttpPost("submitScore")]
        public void Post([FromBody] ScoreDTO score)
        {
            scoreJobManager.AddJobAndContinueJob("score",
                () => services.ProcessScoresJob(score),
                () => services.CheckForCheaters());
        }

        [HttpGet("simulate")]
        public void Simulate()
        {
            //scoreJobManager.AddJob("score",() => services.FailJob());

            //for(int i = 0; i < 1000; i++)
            //{
            //    var randomScore = new ScoreDTO
            //    {
            //        Points = (int)new Random().NextInt64(0, 1000),
            //        PlayerName = $"Player{new Random().Next(1, 100)}",
            //    };
            //    scoreJobManager.AddJobAndContinueJob("score",
            //        () => services.ProcessScoresJob(randomScore),
            //        () => services.CheckForCheaters());
            //}
            scoreJobManager.AddJobAndContinueJob("score",
                () => services.Add1000Records(),
                () => services.CheckForCheaters());
        }

    }
}

