using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Faktury.models
{
    public class Party
    {
        public int Id { get; set; }

        public string Role { get; set; }
        public string Eori { get; set; }
        public string Nip { get; set; }
        public string Name { get; set; }

        public Address MainAddress { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public ContactInfo Contact { get; set; }

        public string CustomerNumber { get; set; }

    }
}
