using Faktury;
using Faktury.models;
using Microsoft.EntityFrameworkCore;


string xmlFilePath = Path.GetFullPath(@"..\..\..\3752528478-20260330-47A0E6000000-3C.xml");

var mapper = new InvoiceMapper();
var invoice = mapper.MapXmlToInvoice(xmlFilePath);
var service = new InvoiceInsert();
service.SaveInvoiceToDatabase(invoice);