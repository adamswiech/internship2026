using System;
using System.Collections.Generic;
using System.Text;

namespace KSeF_App.Server.Models
{
    // implemented
    public class InvoiceLine
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public string Name { get; set; }
        public decimal PricePerPiceNetto { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public int TaxRate { get; set; }
        public string PriceTotalNetto { get; set; }
        public decimal TaxValue { get; set; }

    }
}
