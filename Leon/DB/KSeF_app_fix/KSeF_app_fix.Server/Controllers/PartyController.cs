using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KSeF_app_fix.Server.Services;
using KSeF_app_fix.Server.Models;

namespace KSeF_app_fix.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartyController :ControllerBase
    {
        private readonly PartyService _service;

        public PartyController(PartyService service)
        {
            _service = service;
        }

        [HttpGet("GetPartyById={id}")]
        public async Task<ActionResult<Party>> GetById(int id)
        {
            var Party = await _service.GetByIdAsync(id);

            if (Party == null)
                return NotFound();

            return Ok(Party);
        }
    }
}
