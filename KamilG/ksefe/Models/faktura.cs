using System;
using System.Collections.Generic;

namespace ksefe.Models
{
    public class faktura
    {
        public int id { get; set; }
        public int podmiot1Id { get; set; }
        public int podmiot2Id { get; set; }
        public string kodWaluty { get; set; }
        public DateTime p_1 { get; set; }
        public string p_2 { get; set; }
        public DateTime? p_6Od { get; set; }
        public DateTime? p_6Do { get; set; }
        public decimal p_13_1 { get; set; }
        public decimal p_14_1 { get; set; }
        public decimal p_14W { get; set; }
        public decimal p_15 { get; set; }

        public podmiot podmiot1 { get; set; }
        public podmiot podmiot2 { get; set; }
        public List<faWiersz> wiersze { get; set; } = new List<faWiersz>();
    }
}
