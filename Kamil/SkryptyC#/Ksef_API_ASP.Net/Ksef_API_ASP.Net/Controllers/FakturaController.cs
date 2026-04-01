using Ksef.Models;
using Ksef_API_ASP.Net.Models;
using Ksef_ASP.net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Ksef_ASP.net.Controllers;

[ApiController]
[Route("[controller]")]
public class FakturaController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Json(new object());
    }
    [HttpGet("GetFaktura")]
    public ActionResult<List<FakturaDTO>> GetFaktura()
    {
        var sprzedawca = new Podmiot
        {
            Nip = "1234567890",
            Nazwa = "Firma Sprzedawca Sp. z o.o.",
            KodKraju = "PL",
            AdresL1 = "ul. Kwiatowa 1, 00-001 Warszawa"
        };

        var nabywca = new Podmiot
        {
            Nip = "9876543210",
            Nazwa = "Firma Nabywca S.A.",
            KodKraju = "PL",
            AdresL1 = "ul. Długa 5, 30-002 Kraków"
        };

        var w1 = new FaWiersz { NrWiersza = 1, KursWaluty = 1m, P_7 = "Usługa programistyczna", P_8A = 1m, P_8B = 10m, P_9A = 200m, P_11 = 2000m, P_12 = 460m };
        var w2 = new FaWiersz { NrWiersza = 2, KursWaluty = 1m, P_7 = "Konsultacja", P_8A = 1m, P_8B = 2m, P_9A = 500m, P_11 = 1000m, P_12 = 230m };

        var fakt1 = new Faktura
        {
            Podmiot1 = sprzedawca,
            Podmiot2 = nabywca,
            KodWaluty = "PLN",
            P_1 = DateTime.Today,
            P_2 = "FV/2026/001",
            P_6_Od = DateTime.Today.AddDays(-10),
            P_6_Do = DateTime.Today.AddDays(-9),
            P_13_1 = 3000m,
            P_14_1 = 690m,
            P_14_W = 690m,
            P_15 = 3690m,
            FaWiersze = new List<FaWiersz> { w1, w2 }
        };

        var zagraniczny = new Podmiot
        {
            Nip = "DE123456789",
            Nazwa = "German Client GmbH",
            KodKraju = "DE",
            AdresL1 = "Musterstrasse 10, 10115 Berlin"
        };

        var w3 = new FaWiersz { NrWiersza = 1, KursWaluty = 4.5m, P_7 = "Szkolenie zdalne", P_8A = 1m, P_8B = 1m, P_9A = 1000m, P_11 = 1000m, P_12 = 0m };

        var fakt2 = new Faktura
        {
            Podmiot1 = sprzedawca,
            Podmiot2 = zagraniczny,
            KodWaluty = "EUR",
            P_1 = DateTime.Today.AddDays(-5),
            P_2 = "FV/2026/002",
            P_6_Od = DateTime.Today.AddDays(-15),
            P_6_Do = DateTime.Today.AddDays(-14),
            P_13_1 = 1000m,
            P_14_1 = 0m,
            P_14_W = 0m,
            P_15 = 4500m,
            FaWiersze = new List<FaWiersz> { w3 }
        };

        var facturaList = new List<Faktura> { fakt1, fakt2 };
        var fakturaListDTO = new List<FakturaDTO>();

        foreach (var f in facturaList)
        {
            var newf = new FakturaDTO
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
                FaWiersze = new List<FaWierszDTO>()
            };
            foreach (var w in f.FaWiersze)
            {
                newf.FaWiersze.Add(new FaWierszDTO
                {
                    NrWiersza = w.NrWiersza,
                    KursWaluty = w.KursWaluty,
                    NazwaUslugi = w.P_7,
                    Miara = w.P_8A,
                    Ilosc = w.P_8B,
                    CenaJednostkowa = w.P_9A,
                    WartoscSprzedazyNetto = w.P_11,
                    WartoscPodatkuVat = w.P_12
                });
            }
            fakturaListDTO.Add(newf);
        }
        return fakturaListDTO;
    }
    [HttpGet("GetPodmiot")]
    public IActionResult GetPodmiot()
    {
        return Json(new Podmiot
        {
            Nip = "DE123456789",
            Nazwa = "German Client GmbH",
            KodKraju = "DE",
            AdresL1 = "Musterstrasse 10, 10115 Berlin"
        }, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = null
        }); ;
    }

    [HttpGet("GetIloscFaktur")]
    public ActionResult<int> GetIloscFaktur()
    {
        return 1;
    }

    [HttpGet("GetCzyIstniejeFaktura")]
    public ActionResult<bool> GetCzyIstniejeFaktura()
    {
        return false;
    }
}
