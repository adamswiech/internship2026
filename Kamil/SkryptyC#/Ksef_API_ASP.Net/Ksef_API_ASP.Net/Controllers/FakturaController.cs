using Ksef.Models;
using Ksef_API_ASP.Net.Models;
using Ksef_ASP.net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;

namespace Ksef_ASP.net.Controllers;

[ApiController]
[Route("[controller]")]
public class FakturaController : Controller
{
    private readonly AppDbContext _context;

    public FakturaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetFaktura")]
    public ActionResult<List<FakturaDTO>> GetFaktura()
    {
        return NoContent();
        //var result = _context.Faktura
        //    .Select(f => new FakturaDTO
        //    {
        //        Sprzedawca = new PodmiotDTO
        //        {
        //            Nip = f.Podmiot1.Nip,
        //            Nazwa = f.Podmiot1.Nazwa,
        //            KodKraju = f.Podmiot1.KodKraju,
        //            AdresL1 = f.Podmiot1.AdresL1
        //        },
        //        Nabywca = new PodmiotDTO
        //        {
        //            Nip = f.Podmiot2.Nip,
        //            Nazwa = f.Podmiot2.Nazwa,
        //            KodKraju = f.Podmiot2.KodKraju,
        //            AdresL1 = f.Podmiot2.AdresL1
        //        },
        //        KodWaluty = f.KodWaluty,
        //        DataWyslania = f.P_1,
        //        NrFaktury = f.P_2,
        //        DataOd = f.P_6_Od,
        //        DataDo = f.P_6_Do,
        //        KwatoaNetto = f.P_13_1,
        //        KwotaPodatku = f.P_14_1,
        //        KwotaPodatkuPLN = f.P_14_W,
        //        KwotaNaloznosci = f.P_15,
        //        FaWiersze = f.FaWiersze.Select(w =>new FaWierszDTO
        //            {
        //                NrWiersza = w.NrWiersza,
        //                KursWaluty = w.KursWaluty,
        //                NazwaUslugi = w.P_7,                 
        //                Miara = w.P_8A,
        //                Ilosc = w.P_8B,
        //                CenaJednostkowa = w.P_9A,
        //                WartoscSprzedazyNetto = w.P_11,
        //                WartoscPodatkuVat = w.P_12

        //            }).ToList(),
        //    })
        //    .AsEnumerable()
        //    .ToList();

        //return result;
    }

    [HttpPost("InsertFakturaFromXml")]
    public ActionResult<FakturaDTO> InsertFakturaFromXml([FromBody] string xml)
    {
        return NoContent();
    }

    [HttpGet("GetPodmiot")]
    public ActionResult<List<Podmiot>> GetPodmiot()
    {
        return _context.Podmiot.ToList();
    }

}
