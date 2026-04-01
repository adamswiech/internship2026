using System;
using System.Collections.Generic;
using System.Text;

namespace KSeF_app.Server.Models
{
    // implemented
    public class TaxSummary
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string TaxRate { get; set; }
        public decimal Netto { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Brutto { get; set; } 
        public decimal PLNAmount { get; set; }

    }
}
