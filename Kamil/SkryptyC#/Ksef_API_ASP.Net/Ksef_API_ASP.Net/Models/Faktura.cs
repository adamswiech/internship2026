using System;
using System.Collections.Generic;
using System.Text;

namespace Ksef.Models
{
    public class Faktura
    {
        public int Id { get; set; }

        public required Podmiot Podmiot1 { get; set; } //sprzedawca
        public required Podmiot Podmiot2 { get; set; } //nabywca
        public string KodWaluty { get; set; }
        public DateTime P_1 { get; set; } // data wysłania
        public string P_2 { get; set; } // nr faktury
        public DateTime P_6_Od { get; set; } //data świadczonej usługi
        public DateTime P_6_Do { get; set; }
        public decimal P_13_1 { get; set; } // kwota netto
        public decimal P_14_1 { get; set; } // Kwota podatku
        public decimal P_14_W { get; set; } // kwota podatku PLN
        public decimal P_15 { get; set; } // kwota Kwota należności 

        public required List<FaWiersz> FaWiersze { get; set; }
    }
}
