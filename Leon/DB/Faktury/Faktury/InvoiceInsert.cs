using Faktury.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faktury
{
    public class InvoiceInsert
    {
        public void SaveInvoiceToDatabase(Invoice invoice)
        {
            using (var db = new InvoiceContext())
            {
                bool exists = db.Invoices
                    .Any(i => i.KsefNumber == invoice.KsefNumber);

                if (exists)
                    return;

                if (invoice.Seller != null)
                {
                    if (invoice.Seller.MainAddress != null)
                        db.Addresses.Add(invoice.Seller.MainAddress);

                    if (invoice.Seller.Contact != null)
                        db.ContactInfos.Add(invoice.Seller.Contact);

                    db.Parties.Add(invoice.Seller);
                }

                if (invoice.Buyer != null)
                {
                    if (invoice.Buyer.MainAddress != null)
                        db.Addresses.Add(invoice.Buyer.MainAddress);

                    if (invoice.Buyer.Contact != null)
                        db.ContactInfos.Add(invoice.Buyer.Contact);

                    db.Parties.Add(invoice.Buyer);
                }

                db.Invoices.Add(invoice);

                if (invoice.Lines != null)
                    db.InvoiceLines.AddRange(invoice.Lines);

                if (invoice.TaxSummaries != null)
                    db.TaxSummaries.AddRange(invoice.TaxSummaries);

                if (invoice.Payment != null)
                    db.PaymentInfos.Add(invoice.Payment);

                if (invoice.Settlement != null)
                    db.Settlements.Add(invoice.Settlement);

                if (invoice.TransactionTerms != null)
                    db.Terms.Add(invoice.TransactionTerms);

                db.SaveChanges();
            }
        }
    }
}