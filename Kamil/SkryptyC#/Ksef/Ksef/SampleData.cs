using System;
using System.Collections.Generic;
using Ksef.Models;

namespace Ksef
{
    internal static class SampleData
    {
        public static List<Faktura> GetFaktury()
        {
            var sprzedawca = new Podmiot
            {
                //Id = 1,
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

            var w1 = new FaWiersz
            {
                NrWiersza = 1,
                KursWaluty = 1m,
                P_7 = "Usługa programistyczna",
                P_8A = 1m,
                P_8B = 10m,
                P_9A = 200m,
                P_11 = 2000m,
                P_12 = 460m
            };

            var w2 = new FaWiersz
            {
                NrWiersza = 2,
                KursWaluty = 1m,
                P_7 = "Konsultacja",
                P_8A = 1m,
                P_8B = 2m,
                P_9A = 500m,
                P_11 = 1000m,
                P_12 = 230m
            };

            var fakt1 = new Faktura
            {
                //Id = 1,
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
                //Id = 3,
                Nip = "DE123456789",
                Nazwa = "German Client GmbH",
                KodKraju = "DE",
                AdresL1 = "Musterstrasse 10, 10115 Berlin"
            };

            var w3 = new FaWiersz
            {
                //Id = 3,
                NrWiersza = 1,
                KursWaluty = 4.5m,
                P_7 = "Szkolenie zdalne",
                P_8A = 1m,
                P_8B = 1m,
                P_9A = 1000m,
                P_11 = 1000m,
                P_12 = 0m
            };

            var fakt2 = new Faktura
            {
                //Id = 2,
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
                P_15 = 1000m * 4.5m, // przeliczona na PLN przy kursie
                FaWiersze = new List<FaWiersz> { w3 }
            };

            return new List<Faktura> { fakt1, fakt2 };
        }
    }
}
