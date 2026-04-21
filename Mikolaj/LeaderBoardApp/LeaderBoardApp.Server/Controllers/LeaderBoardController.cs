using LeaderBoardApp.Server.Data;
using LeaderBoardApp.Server.Models;
using LeaderBoardApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaderBoardApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderBoardController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly LeaderBoardService _leaderBoardService;

        public LeaderBoardController(
            AppDbContext context,
            LeaderBoardService leaderBoardService
            )
        {
            _context = context;
            _leaderBoardService = leaderBoardService;
        }

        [HttpPost("uploadScore")]
        public IActionResult UploadScore([FromBody] Player player)
        {
            //BackgroundJob.Enqueue<LeaderBoardService>(service => service.ProcessScore(player));
            _leaderBoardService.QueuePlayerScore(player); //same as above but using service file
            return Ok("Job enqueued.");
        }


        [HttpGet("getAllScores")]
        public ActionResult<IEnumerable<Player>> GetAllScores()
        {
            //FUNCTION ONLY TO GET ALL PLAYERS WITHOUT USING HANGFIRE
            return Ok(_context.PlayersSet.AsNoTracking().ToList());
        }
    }
}
