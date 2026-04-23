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
            try
            {
                _leaderBoardService.QueuePlayerScore(player);
                return Ok("Job enqueued.");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        //[HttpGet("getAllScores")]
        //public ActionResult<IEnumerable<Player>> GetAllScores()
        //{
        //    //FUNCTION ONLY TO GET ALL PLAYERS WITHOUT USING HANGFIRE
        //    return Ok(_context.PlayersSet.AsNoTracking().ToList());
        //}

        [HttpGet("leaderboard")]
        public ActionResult<IEnumerable<Player>> GetLeaderBoard(string gameMode)
        {
            var scores = LeaderBoardService.CachedLeaderBoard
                .Where(p => string.IsNullOrEmpty(gameMode) || p.gameMode == gameMode)
                .ToList();

            if (!scores.Any())
            {
                return Ok(_context.PlayersSet
                    .AsNoTracking()
                    .Where(p => string.IsNullOrEmpty(gameMode) || p.gameMode == gameMode)
                    .OrderByDescending(p => p.score)
                    .Take(10)
                    .ToList());
            }

            return Ok(scores);
        }
    }
}
