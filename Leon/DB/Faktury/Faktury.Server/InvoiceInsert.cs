using Faktury;
using Faktury.Server.models;

public class InvoiceImporter
{
    private readonly InvoiceContext _db;

    public InvoiceImporter(InvoiceContext db)
    {
        _db = db;
    }

    public void ImportInvoice(string xmlPath)
    {
        var mapper = new InvoiceMapper();
        var invoice = mapper.MapXmlToInvoice(xmlPath);

        if (invoice == null)
            throw new Exception("Invalid XML or missing <Faktura> element");

        _db.Invoices.Add(invoice);
        _db.SaveChanges();
    }
}