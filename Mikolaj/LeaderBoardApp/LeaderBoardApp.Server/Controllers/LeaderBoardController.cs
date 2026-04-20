using Hangfire;
using LeaderBoardApp.Server.Data;
using LeaderBoardApp.Server.Models;
using LeaderBoardApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoardApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderBoardController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly AppDbContext _context;
        private readonly LeaderBoardService _leaderBoardService;

        public LeaderBoardController(
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            AppDbContext context,
            LeaderBoardService leaderBoardService
            )
        {
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _context = context;
            _leaderBoardService = leaderBoardService;
        }

        [HttpPost("uploadScore")]
        public IActionResult UploadScore(Player player)
        {
            _leaderBoardService.QueuePlayerScore(player);
            return Ok("Job enqueued.");
        }


        [HttpGet("getAllScores")]
        public ActionResult<IEnumerable<Player>> GetAllScores()
        {
            var scores = _context.PlayersSet.ToList();
            return Ok(scores);
        }
    }
}
