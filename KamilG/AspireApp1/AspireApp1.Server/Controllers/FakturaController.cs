using AspireApp1.Server.Data;
using AspireApp1.Server.Models;
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
        [ProducesResponseType(typeof(List<faktura>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<faktura>>> GetAll()
            => Ok(await _dbContext.Faktura
                .Select(f => new faktura
                {
                    id = f.id,
                    podmiot1Id = f.podmiot1Id,
                    podmiot2Id = f.podmiot2Id,
                    kodWaluty = f.kodWaluty,
                    p_1 = f.p_1,
                    p_2 = f.p_2,
                    p_13_1 = f.p_13_1,
                    p_14_1 = f.p_14_1,
                    p_15 = f.p_15,
                    wiersze = f.wiersze
                })
                .ToListAsync());

    }
}
