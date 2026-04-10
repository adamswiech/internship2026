using Ksef.Models;
using Ksef_API_ASP.Net.Models;
using Ksef_ASP.net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

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
        var result = _context.Faktura
            .Select(f => new FakturaDTO
            {
                Sprzedawca = new PodmiotDTO
                {
                    Nip = f.Podmiot1.Nip,
                    Nazwa = f.Podmiot1.Nazwa,
                    KodKraju = f.Podmiot1.KodKraju,
                    AdresL1 = f.Podmiot1.AdresL1
                },
                Nabywca = new PodmiotDTO
                {
                    Nip = f.Podmiot2.Nip,
                    Nazwa = f.Podmiot2.Nazwa,
                    KodKraju = f.Podmiot2.KodKraju,
                    AdresL1 = f.Podmiot2.AdresL1
                },
                KodWaluty = f.KodWaluty,
                DataWyslania = f.P_1,
                NrFaktury = f.P_2,
                DataOd = f.P_6_Od,
                DataDo = f.P_6_Do,
                KwatoaNetto = f.P_13_1,
                KwotaPodatku = f.P_14_1,
                KwotaPodatkuPLN = f.P_14_W,
                KwotaNaloznosci = f.P_15,
                FaWiersze = f.FaWiersze.Select(w => new FaWierszDTO
                {
                    NrWiersza = w.NrWiersza,
                    KursWaluty = w.KursWaluty,
                    NazwaUslugi = w.P_7,
                    Miara = w.P_8A,
                    Ilosc = w.P_8B,
                    CenaJednostkowa = w.P_9A,
                    WartoscSprzedazyNetto = w.P_11,
                    WartoscPodatkuVat = w.P_12

                }).ToList(),
            })
            .AsEnumerable()
            .ToList();

        return result;
    }

    [HttpPost("InsertFakturaFromXml")]
    public ActionResult<FakturaDTO> InsertFakturaFromXml([FromBody] string xmlString)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(KsefFaktura));
        using var reader = new StringReader(xmlString);
        var xml = (KsefFaktura)xmlSerializer.Deserialize(reader);


        Faktura faktura = new Faktura
        {
            Podmiot1 = new Podmiot
            {
                Nip = xml.Podmiot1.DaneIdentyfikacyjne.NIP,
                Nazwa = xml.Podmiot1.DaneIdentyfikacyjne.Nazwa,
                KodKraju = xml.Podmiot1.Adres.KodKraju,
                AdresL1 = xml.Podmiot1.Adres.AdresL1,
            } ,
            Podmiot2 = new Podmiot
            {
                Nip = xml.Podmiot2.DaneIdentyfikacyjne.NIP,
                Nazwa = xml.Podmiot2.DaneIdentyfikacyjne.Nazwa,
                KodKraju = xml.Podmiot2.Adres.KodKraju,
                AdresL1 = xml.Podmiot2.Adres.AdresL1,
            },
            KodWaluty = xml.Fa.KodWaluty,
            P_1 = xml.Fa.P_1,
            P_2 = xml.Fa.P_2,
            P_6_Od = xml.Fa.OkresFa.P_6_Od,
            P_6_Do = xml.Fa.OkresFa.P_6_Do,
            P_13_1 = xml.Fa.P_13_1,
            P_14_1 = xml.Fa.P_14_1,
            P_14_W = xml.Fa.P_14_1W,
            P_15 = xml.Fa.P_15,
            FaWiersze = xml.Fa.FaWiersz.Select(w => new FaWiersz
            {
                NrWiersza = w.NrWierszaFa,
                P_7 = w.P_7,
                P_8A = w.P_8A,
                P_8B = w.P_8B,
                P_9A = w.P_9A,
                P_11 = w.P_11,
                P_12 = w.P_12,
                KursWaluty = w.KursWaluty
            }).ToList()
        };
        _context.Faktura.Add(faktura);

        _context.SaveChanges();

        return new FakturaDTO
        {
            Sprzedawca = new PodmiotDTO
            {
                Nip = faktura.Podmiot1.Nip,
                Nazwa = faktura.Podmiot1.Nazwa,
                KodKraju = faktura.Podmiot1.KodKraju,
                AdresL1 = faktura.Podmiot1.AdresL1
            },
            Nabywca = new PodmiotDTO
            {
                Nip = faktura.Podmiot2.Nip,
                Nazwa = faktura.Podmiot2.Nazwa,
                KodKraju = faktura.Podmiot2.KodKraju,
                AdresL1 = faktura.Podmiot2.AdresL1
            },
            KodWaluty = faktura.KodWaluty,
            DataWyslania = faktura.P_1,
            NrFaktury = faktura.P_2,
            DataOd = faktura.P_6_Od,
            DataDo = faktura.P_6_Do,
            KwatoaNetto = faktura.P_13_1,
            KwotaPodatku = faktura.P_14_1,
            KwotaPodatkuPLN = faktura.P_14_W,
            KwotaNaloznosci = faktura.P_15,
            FaWiersze = faktura.FaWiersze.Select(w => new FaWierszDTO
            {
                NrWiersza = w.NrWiersza,
                KursWaluty = w.KursWaluty,
                NazwaUslugi = w.P_7,
                Miara = w.P_8A,
                Ilosc = w.P_8B,
                CenaJednostkowa = w.P_9A,
                WartoscSprzedazyNetto = w.P_11,
                WartoscPodatkuVat = w.P_12

            }).ToList(),
        };
    }

    [HttpGet("GetPodmiot")]
    public ActionResult<List<Podmiot>> GetPodmiot()
    {
        return _context.Podmiot.ToList();
    }

}
