using System;
using System.Collections.Generic;
using System.Text;

namespace KSeF_app_fix.Server.Models
{
    // implemented
    public class Invoice
    {
        public int Id { get; set; }

        public string InvoiceNumber { get; set; }
        public string KsefNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? IssuePlace { get; set; }

        public string? CurrencyCode { get; set; }
        public decimal? CurrencyRate { get; set; }





        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public Party Seller { get; set; }
        public Party Buyer { get; set; }
        public List<Party>? OtherParties { get; set; } = new();

        public List<InvoiceLine>? Lines { get; set; } = new();
        public List<TaxSummary>? TaxSummaries { get; set; } = new();

        public PaymentInfo? Payment { get; set; }
        public Settlement? Settlement { get; set; }

        public int? FactorBankAccountId { get; set; }
        public BankAccount? FactorBankAccount { get; set; }
        public int? SellerBankAccountId { get; set; }
        public BankAccount? SellerBankAccount { get; set; }

        public Terms? TransactionTerms { get; set; }

        public string? FooterNote { get; set; }

    }
}
