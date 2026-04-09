using KSeF_appliction.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
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

        [HttpPost("AddXML")]
        public ActionResult AddXML(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                using var stream = file.OpenReadStream();
                var doc = XDocument.Load(stream);

                if (doc.Root == null)
                    return BadRequest("XML root is null.");
                var fakturaElements = doc.Root.Name.LocalName == "Faktury"
                    ? doc.Root.Elements("Faktura").ToList()
                    : new List<XElement> { doc.Root };

                if (!fakturaElements.Any())
                    return BadRequest($"No <Faktura> elements found. Root is <{doc.Root.Name.LocalName}>.");

                foreach (var root in fakturaElements)
                {
                    var podmiot1El = root.Element("podmiot1");
                    var podmiot2El = root.Element("podmiot2");

                    if (podmiot1El == null) return BadRequest("Missing <podmiot1> element.");
                    if (podmiot2El == null) return BadRequest("Missing <podmiot2> element.");

                    var podmiot1 = new Podmiot
                    {
                        Nip = podmiot1El.Element("Nip")?.Value,
                        Nazwa = podmiot1El.Element("Nazwa")?.Value,
                        AdresL1 = podmiot1El.Element("AdresL1")?.Value,
                        KodKraju = podmiot1El.Element("KodKraju")?.Value
                    };

                    var podmiot2 = new Podmiot
                    {
                        Nip = podmiot2El.Element("Nip")?.Value,
                        Nazwa = podmiot2El.Element("Nazwa")?.Value,
                        AdresL1 = podmiot2El.Element("AdresL1")?.Value,
                        KodKraju = podmiot2El.Element("KodKraju")?.Value
                    };

                    _db.Podmiot.Add(podmiot1);
                    _db.Podmiot.Add(podmiot2);
                    _db.SaveChanges();
                    var wiersze = root.Element("Wiersze")?
                        .Elements("FaWiersz")
                        .Select(w => new FaWiersz
                        {
                            Nr_wiersza = int.Parse(w.Element("Nr_wiersza")?.Value ?? "0"),
                            P_7 = w.Element("P_7")?.Value,
                            P_8A = int.Parse(w.Element("P_8A")?.Value ?? "0"),
                            P_8B = int.Parse(w.Element("P_8B")?.Value ?? "0"),
                            P_9A = decimal.Parse(w.Element("P_9A")?.Value ?? "0", CultureInfo.InvariantCulture),
                            P_11 = decimal.Parse(w.Element("P_11")?.Value ?? "0", CultureInfo.InvariantCulture),
                            P_12 = int.Parse(w.Element("P_12")?.Value ?? "0"),
                            Kurs_waluty = decimal.Parse(w.Element("Kurs_waluty")?.Value ?? "1", CultureInfo.InvariantCulture)
                        }).ToList();

                    var faktura = new Faktura
                    {
                        podmiot1 = podmiot1,
                        podmiot2 = podmiot2,
                        Podmiot1Id = podmiot1.Id,
                        Podmiot2Id = podmiot2.Id,
                        Kod_waluty = root.Element("Kod_waluty")?.Value,
                        P_1 = DateTime.Parse(root.Element("P_1")?.Value ?? DateTime.MinValue.ToString()),
                        P_2 = root.Element("P_2")?.Value,
                        P_6_Od = DateTime.Parse(root.Element("P_6_Od")?.Value ?? DateTime.MinValue.ToString()),
                        P_6_Do = DateTime.Parse(root.Element("P_6_Do")?.Value ?? DateTime.MinValue.ToString()),
                        P_13_1 = decimal.Parse(root.Element("P_13_1")?.Value ?? "0", CultureInfo.InvariantCulture),
                        P_14_1 = decimal.Parse(root.Element("P_14_1")?.Value ?? "0", CultureInfo.InvariantCulture),
                        P_14_W = decimal.Parse(root.Element("P_14_W")?.Value ?? "0", CultureInfo.InvariantCulture),
                        P_15 = decimal.Parse(root.Element("P_15")?.Value ?? "0", CultureInfo.InvariantCulture),
                        Wiersze = wiersze
                    };

                    _db.Faktura.Add(faktura);
                    _db.SaveChanges();
                }

                return Ok($"Zaimportowano {fakturaElements.Count} faktur.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = e.Message,
                    inner = e.InnerException?.Message,
                    source = e.Source,
                    stack = e.StackTrace
                });
            }
        }

        [HttpGet("GetFaktury")]
        public ActionResult<List<Faktura>> GetFaktury()
        {
            var faktury = _db.Faktura
                .Include(f => f.podmiot1)
                .Include(f => f.podmiot2)
                .Include(f => f.Wiersze)
                .ToList();

            return faktury;
        }


        [HttpGet("GetFaktura")]
        public ActionResult<Faktura> GetFaktura(int fakturaId)
        {
            var faktura = _db.Faktura
                .Include(f => f.podmiot1)
                .Include(f => f.podmiot2)
                .Include(f => f.Wiersze)
                .FirstOrDefault(f => f.Id == fakturaId);

            if (faktura == null)
            {
                return NotFound($"Faktura with ID {fakturaId} not found.");
            }

            return faktura;
        }
    }
}