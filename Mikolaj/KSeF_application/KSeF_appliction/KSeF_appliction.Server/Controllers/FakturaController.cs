
using KSeF_appliction.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace KSeF_appliction.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FakturaController : ControllerBase
    {
        private readonly AppDbContext _db;

        public FakturaController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var doc = XDocument.Load(stream);

            var faktura = new Faktura
            {
                Id = int.Parse(doc.Root.Element("Id")?.Value ?? "0"),
                Podmiot1Id = int.Parse(doc.Root.Element("Podmiot1Id")?.Value ?? "0"),
                Podmiot2Id = int.Parse(doc.Root.Element("Podmiot1Id")?.Value ?? "0"),
            };

            _db.Faktura.Add(faktura);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), new { id = faktura.Id }, faktura);
        }

        [HttpGet("GetFaktury")]
        public async Task<IActionResult> GetFaktury()
        {
            var faktury = await _db.Faktura
        .Include(f => f.podmiot1)
        .Include(f => f.podmiot2)
        .Include(f => f.Wiersze)
        .ToListAsync();
            return Ok(faktury);
        }
    }
}

