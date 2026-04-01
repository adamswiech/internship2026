using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace KSeF_app.Server.Models
{
    // implemented
    public class Settlement
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public List<Charge> Charges { get; set; } = new();
        public List<Deduction> Deductions { get; set; } = new();

        public decimal TotalToPay { get; set; }
    }

    public class Charge
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public int SettlementId { get; set; }
        public Settlement Settlement { get; set; }

    }

    public class Deduction
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public int SettlementId { get; set; }
        public Settlement Settlement { get; set; }

    }
}
