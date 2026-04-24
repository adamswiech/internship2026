using KSeF_app_fix.Server.Data;
using KSeF_app_fix.Server.Models;
using KSeF_app_fix.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KSeF_app_fix.Server.Models
{

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

    public class InvoiceDTOs
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string SellerName { get; set; } // party
        public string BuyerName { get; set; }   // party
        public decimal TotalAmount { get; set; } // settlement
    }
}

