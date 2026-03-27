using System;
using System.Collections.Generic;
using System.Text;

namespace Insert1000000.Models
{
    public class Adres
    {
        public string Kraj { get; set; }
        public string Wojewodztwo { get; set; }
        public string Miasto { get; set; }
        public Mieszkanie Mieszkanie { get; set; }
    }
}
