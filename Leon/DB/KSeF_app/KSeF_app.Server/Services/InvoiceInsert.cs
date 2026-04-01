using KSeF_app.Server.Models;
using KSeF_app.Server.Data;

public class InvoiceInsert
{
    private readonly KsefContext _db;

    public InvoiceInsert(KsefContext db)
    {
        _db = db;
    }

    public async Task<int> SaveInvoiceAsync(Invoice invoice)
    {
        _db.Invoices.Add(invoice);

        await _db.SaveChangesAsync();

        return invoice.Id;
    }
}