using KSeF_app_fix.Server.Data;
using KSeF_app_fix.Server.Models;
using Microsoft.EntityFrameworkCore;

public class InvoiceService
{
    private readonly KsefContext _db;

    public InvoiceService(KsefContext db)
    {
        _db = db;
    }
    public async Task<int> SaveInvoiceAsync(Invoice invoice)
    {
        _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
        return invoice.Id;
    }

    public async Task<List<Invoice>> GetAllInvoicesAsync()
    {
        return await _db.Invoices
        .Include(i => i.Seller)
        .Include(i => i.Buyer)
        .Include(i => i.OtherParties)
        .Include(i => i.Lines)
        .Include(i => i.TaxSummaries)
        .Include(i => i.Payment)
        .Include(i => i.Settlement)
        .Include(i => i.FactorBankAccount)
        .Include(i => i.SellerBankAccount)
        .Include(i => i.TransactionTerms)
        .ToListAsync();
    }
    public async Task<List<InvoiceDTOs>> GetAllInvoiceDTOsAsync()
    {
        return await _db.Invoices.Include(i => i.Seller).Include(i => i.Buyer).Include(i => i.Settlement).Select(i => new InvoiceDTOs
        {
            Id = i.Id,
            InvoiceNumber = i.InvoiceNumber,
            IssueDate = i.IssueDate,
            SellerName = i.Seller.Name ?? "null",
            BuyerName = i.Buyer.Name ?? "null",
            TotalAmount = i.Settlement == null ? 0m : i.Settlement.TotalToPay
        }).ToListAsync();
    }
    public async Task<Invoice?> GetByIdAsync(int Id)
    {
        return await _db.Invoices
        .Include(i => i.Seller)
        .Include(i => i.Buyer)
        .Include(i => i.OtherParties)
        .Include(i => i.Lines)
        .Include(i => i.TaxSummaries)
        .Include(i => i.Payment)
        .Include(i => i.Settlement)
        .Include(i => i.FactorBankAccount)
        .Include(i => i.SellerBankAccount)
        .Include(i => i.TransactionTerms)
        .FirstOrDefaultAsync(x => x.Id ==Id);
    }
}
