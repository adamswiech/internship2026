using GameProcessor.Server.Data;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace GameProcessor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class GameController : ControllerBase
    {
        private readonly DbContext _dbContext;
        public GameController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
