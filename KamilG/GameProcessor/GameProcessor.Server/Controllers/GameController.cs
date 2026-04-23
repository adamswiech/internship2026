using GameProcessor.Server.Data;
using GameProcessor.Server.Jobs;
using GameProcessor.Server.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace GameProcessor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly DbContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public GameController(DbContext dbContext, IBackgroundJobClient backgroundJobClient)
        {
            _dbContext = dbContext;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAll()
        {
            var games = await _dbContext.GetAllAsync();
            return Ok(games);
        }

        [HttpGet("leaderboard")]
        public async Task<ActionResult<IEnumerable<Game>>> GetLeaderboard(
            [FromQuery] string? gameMode = null,
            [FromQuery] int top = 10)
        {
            if (top <= 0 || top > 100)
                top = 10;
            var leaders = 1;

            return Ok(leaders);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Create(Game game)
        {
            _backgroundJobClient.Enqueue<ProcessScore>(job => job.ProcessScoreAsync(game));
            return Accepted();
        }
    }
}