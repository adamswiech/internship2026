using System;
using System.Collections.Generic;
using System.Text;

namespace Ksef.Models
{
    internal class FaWiersz
    {
        public int Id { get; set; }
        public int FakturaId { get; set; }
        public Faktura Faktura { get; set; }
        public int NrWiersza { get; set; }
        public decimal KursWaluty { get; set; }
        public string P_7 { get; set; } //nazwa usługi
        public decimal P_8A { get; set; } //miara
        public decimal P_8B { get; set; } //Ilość
        public decimal P_9A { get; set; } //cena Jednostkowa
        public decimal P_11 { get; set; } //wartosc sprzedazy netto
        public decimal P_12 { get; set; } //wartosc podatku vat
    }
}
