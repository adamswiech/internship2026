using System;
using System.Collections.Generic;
using System.Text;

namespace Ksef.Models
{
    internal class Podmiot
    {
        public int Id { get; set; }
        public string Nip { get; set; }
        public string Nazwa { get; set; }
        public string KodKraju { get; set; }
        public string AdresL1 { get; set; }
    }
}
