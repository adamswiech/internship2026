using KSeF_app.Server.Data;
using KSeF_app.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace KSeF_app.Server.Services
{
    public class InvoiceSelect
    {
        private readonly KsefContext _db;

        public InvoiceSelect(KsefContext db)
        {
            _db = db;
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _db.Invoices.ToListAsync();
        }
    }
}
