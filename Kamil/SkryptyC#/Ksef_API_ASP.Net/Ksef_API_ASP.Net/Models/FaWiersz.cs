using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ksef.Models
{
    public class FaWiersz
    {
        public int Id { get; set; }
        public int FakturaId { get; set; }
        public int NrWiersza { get; set; }

        [Column("KursWaluty", TypeName = "decimal(18, 6)")]
        public decimal KursWaluty { get; set; }
        public string P_7 { get; set; } //nazwa usługi
        public decimal P_8A { get; set; } //miara
        public decimal P_8B { get; set; } //Ilość
        public decimal P_9A { get; set; } //cena Jednostkowa
        public decimal P_11 { get; set; } //wartosc sprzedazy netto
        public decimal P_12 { get; set; } //wartosc podatku vat
    }
}
