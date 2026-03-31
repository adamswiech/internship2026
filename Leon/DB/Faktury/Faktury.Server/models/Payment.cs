using System;
using System.Collections.Generic;
using System.Text;

namespace Faktury.Server.models
{
    // implemented
    public class PaymentInfo
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public bool IsPartial { get; set; }
        public List<PartialPayment> PartialPayments { get; set; } = new();

        public DateTime PaymentDueDate { get; set; }
        public string PaymentTermsDescription { get; set; }

        public string PaymentMethod { get; set; } 
    }
    public class PartialPayment
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
