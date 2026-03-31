using Faktury;
using Faktury.Server.models;





string xmlPath = Path.GetFullPath("./../../../3752528478-20260330-47A0E6000000-3C.xml");

var mapper = new InvoiceMapper();
var invoice = mapper.MapXmlToInvoice(xmlPath);

if (invoice == null)
    throw new Exception("Invalid XML or missing <Faktura> element");

using var db = new InvoiceContext();

db.Invoices.Add(invoice);
db.SaveChanges();
