using AspireApp1.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.Server.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class FakturaController: ControllerBase
    {
        private readonly AspireDbContext _dbContext;

        public FakturaController(AspireDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _dbContext.Faktura
                .Select(f => new
                {
                    f.id,
                    f.podmiot1Id,
                    f.podmiot2Id,
                    f.kodWaluty,
                    f.p_1,
                    f.p_2,
                    f.p_13_1,
                    f.p_14_1,
                    f.p_15,
                    wierszeCount = f.wiersze.Count
                })
                .ToListAsync());

    }
}
