using System;
using System.Collections.Generic;
using System.Text;

namespace KSeF_app.Server.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string CountryCode { get; set; }
        public string Line1 { get; set; }
        public string? Line2 { get; set; }
        public string GLN { get; set; }

    }
}
