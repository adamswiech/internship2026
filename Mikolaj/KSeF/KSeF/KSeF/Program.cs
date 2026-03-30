using KSeF;
using KSeF.Models;

using var dbContext = new AppDbContext();

dbContext.Faktura.Add(new Faktura
{
    podmiot1 = new Podmiot
    {
        Nip = "7272445612",
        Nazwa = "MediaPro Sp. z o.o.",
        AdresL1 = "ul. Piotrkowska 120, 90-006 Łódź",
        KodKraju = "PL"
    },
    podmiot2 = new Podmiot
    {
        Nip = "5262784321",
        Nazwa = "RetailMax S.A.",
        AdresL1 = "ul. Puławska 300, 02-819 Warszawa",
        KodKraju = "PL"
    },
    Kod_waluty = "EUR",
    P_1 = new DateTime(2025, 2, 3),
    P_2 = "FV/2025/02/004",
    P_6_Od = new DateTime(2025, 2, 1),
    P_6_Do = new DateTime(2025, 2, 28),
    P_13_1 = 5500.00m,
    P_14_1 = 1265.00m,
    P_14_W = 1265,
    P_15 = 6765.00m,
    Wiersze = new List<FaWiersz>
    {
        new FaWiersz
        {
            Nr_wiersza = 1,
            P_7 = "Kampania reklamowa Google Ads",
            P_8A = 1,
            P_8B = 1,
            P_9A = 3000.00m,
            P_11 = 3000.00m,
            P_12 = 23,
            Kurs_waluty = 4.2850m
        },
        new FaWiersz
        {
            Nr_wiersza = 2,
            P_7 = "Obsługa mediów społecznościowych",
            P_8A = 1,
            P_8B = 1,
            P_9A = 1500.00m,
            P_11 = 1500.00m,
            P_12 = 23,
            Kurs_waluty = 4.2850m
        },
        new FaWiersz
        {
            Nr_wiersza = 3,
            P_7 = "Produkcja materiałów graficznych",
            P_8A = 5,
            P_8B = 5,
            P_9A = 200.00m,
            P_11 = 1000.00m,
            P_12 = 23,
            Kurs_waluty = 4.2850m
        }
    }
});

dbContext.SaveChanges();