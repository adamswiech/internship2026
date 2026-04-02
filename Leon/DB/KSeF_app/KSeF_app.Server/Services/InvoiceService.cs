using KSeF_App.Server.Data;
using KSeF_App.Server.Models;
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
        return await _db.Invoices.ToListAsync();
    }

    public async Task<Invoice?> GetByIdAsync(int Id)
    {
        return await _db.Invoices.FindAsync(Id);
    }
}