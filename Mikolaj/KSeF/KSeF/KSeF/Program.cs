using KSeF;
using KSeF.Models;

using var dbContext = new AppDbContext();


dbContext.Faktura.Add(new Faktura
{
    podmiot1 = new Podmiot
    {
        Nip = "9521384670",
        Nazwa = "TechSolutions Sp. z o.o.",
        AdresL1 = "ul. Marszałkowska 55, 00-676 Warszawa",
        KodKraju = "PL"
    },
    podmiot2 = new Podmiot
    {
        Nip = "6312047859",
        Nazwa = "BuildCorp S.A.",
        AdresL1 = "ul. Długa 18, 31-147 Kraków",
        KodKraju = "PL"
    },
    Kod_waluty = "PLN",
    P_1 = new DateTime(2025, 3, 5),
    P_2 = "FV/2025/03/011",
    P_6_Od = new DateTime(2025, 3, 1),
    P_6_Do = new DateTime(2025, 3, 31),
    P_13_1 = 12400.00m,
    P_14_1 = 2852.00m,
    P_14_W = 2852,
    P_15 = 15252.00m,
    Wiersze = new List<FaWiersz>
    {
        new FaWiersz
        {
            Nr_wiersza = 1,
            P_7 = "Wdrożenie systemu ERP",
            P_8A = 1,
            P_8B = 1,
            P_9A = 7000.00m,
            P_11 = 7000.00m,
            P_12 = 23,
            Kurs_waluty = 1.0000m
        },
        new FaWiersz
        {
            Nr_wiersza = 2,
            P_7 = "Szkolenie użytkowników systemu",
            P_8A = 4,
            P_8B = 4,
            P_9A = 800.00m,
            P_11 = 3200.00m,
            P_12 = 23,
            Kurs_waluty = 1.0000m
        },
        new FaWiersz
        {
            Nr_wiersza = 3,
            P_7 = "Licencja oprogramowania (12 miesięcy)",
            P_8A = 1,
            P_8B = 1,
            P_9A = 1200.00m,
            P_11 = 1200.00m,
            P_12 = 23,
            Kurs_waluty = 1.0000m
        },
        new FaWiersz
        {
            Nr_wiersza = 4,
            P_7 = "Wsparcie techniczne (godziny)",
            P_8A = 10,
            P_8B = 10,
            P_9A = 100.00m,
            P_11 = 1000.00m,
            P_12 = 23,
            Kurs_waluty = 1.0000m
        }
    }
});

dbContext.SaveChanges();