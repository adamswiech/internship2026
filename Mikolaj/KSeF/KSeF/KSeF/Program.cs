using KSeF;
using KSeF.Models;

using var dbContext = new AppDbContext();

dbContext.Faktury.Add(new Faktura
{
    podmiot1 = new Podmiot
    {
        Nip = "5213876543",
        Nazwa = "TechSoft Sp. z o.o.",
        AdresL1 = "ul. Marszałkowska 10, 00-590 Warszawa",
        KodKraju = "PL"
    },
    podmiot2 = new Podmiot
    {
        Nip = "6462345678",
        Nazwa = "BuildCo S.A.",
        AdresL1 = "ul. Krakowska 45, 30-001 Kraków",
        KodKraju = "PL"
    },
    Kod_waluty = "PLN",
    P_1 = new DateTime(2025, 1, 5),
    P_2 = "FV/2025/01/001",
    P_6_Od = new DateTime(2025, 1, 1),
    P_6_Do = new DateTime(2025, 1, 31),
    P_13_1 = 12000.00m,
    P_14_1 = 2760.00m,
    P_14_W = 2760,
    P_15 = 14760.00m,
    Wiersze = new List<FaWiersz>
    {
        new FaWiersz
        {
            Nr_wiersza = 1,
            P_7 = "Licencja oprogramowania ERP",
            P_8A = 1,
            P_8B = 1,
            P_9A = 8000.00m,
            P_11 = 8000.00m,
            P_12 = 23,
            Kurs_waluty = 1.0000m
        },
        new FaWiersz
        {
            Nr_wiersza = 2,
            P_7 = "Wdrożenie i konfiguracja systemu",
            P_8A = 8,
            P_8B = 8,
            P_9A = 500.00m,
            P_11 = 4000.00m,
            P_12 = 23,
            Kurs_waluty = 1.0000m
        }
    }
});


dbContext.SaveChanges();