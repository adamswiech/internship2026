using Faktury;
using Faktury.models;



string xmlPath = Path.GetFullPath("./../../../3752528478-20260330-47A0E6000000-3C.xml");

var mapper = new InvoiceMapper();
var invoice = mapper.MapXmlToInvoice(xmlPath);

Console.WriteLine(invoice.Buyer.CustomerNumber);
Console.WriteLine(invoice.FactorBankAccountId);
Console.WriteLine(invoice.FactorBankAccount);


using var db = new KsefContext();
var service = new InvoiceInsert(db);
var id = await service.SaveInvoiceAsync(invoice);