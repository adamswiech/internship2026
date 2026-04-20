using Hangfire;
using leaderboardServer.Models;
using leaderboardServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace leaderboardServer.Controllers
{
    [ApiController]
    [Route("api/leaderboard")]
    public class leaderboardController : ControllerBase
    {
        private readonly leaderboardService _service;

        public leaderboardController(leaderboardService service)
        {
            _service = service;
        }

        [HttpPost("addScore")]
        public async Task<ActionResult<Score>> addScore(
            [FromBody] Score score
            )
        {
            var jobId = BackgroundJob.Enqueue(() => _service.processScore(score));
            return Accepted(new { jobId = jobId, message = "Score processing queued" });
        }


        [HttpGet("getTop10")]
        public async Task<ActionResult<List<Top10>>> getTop10()
        {
            var res = await _service.getTop10();
            return Ok(res);

        }


        [HttpPost("simulate")]
        public async Task<ActionResult> simulate()
        {
            var jobId = BackgroundJob.Enqueue(() => _service.simulate());
            return Accepted(new { jobId = jobId, message = "simulation queued" });
        }

        [HttpGet("getSnapshots")]
        public async Task<ActionResult<List<Top10snapshot>>> getSnapshots()
        {
            var res = await _service.getSnapshots();
            return Ok(res);
        }
    }
}
