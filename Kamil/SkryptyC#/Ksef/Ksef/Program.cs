using Ksef;
using Ksef.Models;
using Ksef_API_ASP.Net.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlTypes;
using System.Reflection.Emit;
using System.Xml.Serialization;

using var db = new AppDbContext();

// inserting via c#
var xmlString = File.ReadAllText(@"C:\Users\VULCAN\Documents\Main\GIT\internship2026\Kamil\SkryptyC#\Ksef\Ksef\example.xml");

XmlSerializer xmlSerializer = new XmlSerializer(typeof(KsefFaktura));
using var reader = new StringReader(xmlString);
var xml = (KsefFaktura)xmlSerializer.Deserialize(reader);

//maping
Faktura faktura = new Faktura
{
    Podmiot1 = new Podmiot
    {
        Nip = xml.Podmiot1.DaneIdentyfikacyjne.NIP,
        Nazwa = xml.Podmiot1.DaneIdentyfikacyjne.Nazwa,
        KodKraju = xml.Podmiot1.Adres.KodKraju,
        AdresL1 = xml.Podmiot1.Adres.AdresL1,
    },
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

    db.Faktura.Add(faktura);

db.SaveChanges();

