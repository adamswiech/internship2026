using Faktury.Server.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Faktury
{
    public class InvoiceMapper
    {
        private static readonly XNamespace ns = XNamespace.Get("http://crd.gov.pl/wzor/2025/06/25/13775/");

        public Invoice MapXmlToInvoice(string xmlFilePath)
        {
            XDocument doc = XDocument.Load(xmlFilePath);

            var fakturaElement = doc.Descendants(ns + "Faktura").FirstOrDefault();

            if (fakturaElement == null) return null;

            var faElement = fakturaElement.Descendants(ns + "Fa").FirstOrDefault();
            if (faElement == null) return null;

            var invoice = new Invoice
            {
                InvoiceNumber = faElement.Descendants(ns + "P_2").FirstOrDefault()?.Value,
                KsefNumber = fakturaElement.Descendants(ns + "KodFormularza").FirstOrDefault()?.Value,
                IssueDate = ParseDate(faElement.Descendants(ns + "P_1").FirstOrDefault()?.Value),
                DeliveryDate = ParseDate(faElement.Descendants(ns + "P_6").FirstOrDefault()?.Value),
                IssuePlace = faElement?.Descendants(ns + "P_1M").FirstOrDefault()?.Value,
                CurrencyCode = faElement?.Descendants(ns + "KodWaluty").FirstOrDefault()?.Value,
                CurrencyRate = decimal.Parse(faElement?.Descendants(ns + "KursWalutyZ").FirstOrDefault()?.Value ?? "1"),

                Seller = MapParty(fakturaElement.Descendants(ns + "Podmiot1").FirstOrDefault()),
                Buyer = MapParty(fakturaElement.Descendants(ns + "Podmiot2").FirstOrDefault()),
                OtherParties = fakturaElement.Descendants(ns + "Podmiot3")
                    .Select(p => MapParty(p))
                    .ToList(),

                Lines = faElement?.Descendants(ns + "ZamowienieWiersz")
                    .Select(zw => new InvoiceLine
                    {
                        Name = zw.Descendants(ns + "P_7Z").FirstOrDefault()?.Value,
                        PricePerPiceNetto = decimal.Parse(zw.Descendants(ns + "P_11NettoZ").FirstOrDefault()?.Value ?? "0"),
                        Quantity = int.Parse(zw.Descendants(ns + "P_8BZ").FirstOrDefault()?.Value ?? "0"),
                        Unit = zw.Descendants(ns + "P_8AZ").FirstOrDefault()?.Value,
                        TaxRate = int.Parse(zw.Descendants(ns + "P_12Z").FirstOrDefault()?.Value ?? "0"),
                        PriceTotalNetto = zw.Descendants(ns + "P_11NettoZ").FirstOrDefault()?.Value,
                        TaxValue = decimal.Parse(zw.Descendants(ns + "P_11VatZ").FirstOrDefault()?.Value ?? "0")
                    }).ToList() ?? new List<InvoiceLine>(),

                TaxSummaries = MapTaxSummaries(faElement),

                Payment = MapPayment(faElement?.Descendants(ns + "Platnosc").FirstOrDefault()),
                Settlement = MapSettlement(faElement?.Descendants(ns + "Rozliczenie").FirstOrDefault()),
                SellerBankAccount = MapBankAccount(faElement?.Descendants(ns + "RachunekBankowy").FirstOrDefault()),
                FactorBankAccount = MapBankAccount(faElement?.Descendants(ns + "RachunekBankowyFaktora").FirstOrDefault()),

                TransactionTerms = MapTerms(faElement?.Descendants(ns + "WarunkiTransakcji").FirstOrDefault()),
                FooterNote = fakturaElement.Descendants(ns + "StopkaFaktury").FirstOrDefault()?.Value
            };

            return invoice;
        }

        private List<TaxSummary> MapTaxSummaries(XElement faElement)
        {
            if (faElement == null) return new List<TaxSummary>();

            var summaries = new List<TaxSummary>();
            var p13_1Elements = faElement.Descendants(ns + "P_13_1").ToList();

            for (int i = 0; i < p13_1Elements.Count; i++)
            {
                var taxRate = p13_1Elements[i].Value;
                var p14_1Value = faElement.Descendants(ns + "P_14_1").ElementAtOrDefault(i)?.Value ?? "0";
                var p13_2Value = faElement.Descendants(ns + "P_13_2").ElementAtOrDefault(i)?.Value ?? "0";
                var p15Value = faElement.Descendants(ns + "P_15").ElementAtOrDefault(i)?.Value ?? "0";
                var p14_2WValue = faElement.Descendants(ns + "P_14_2W").ElementAtOrDefault(i)?.Value ?? "0";

                summaries.Add(new TaxSummary
                {
                    TaxRate = taxRate,
                    Netto = decimal.Parse(p13_2Value),
                    TaxAmount = decimal.Parse(p14_1Value),
                    Brutto = decimal.Parse(p15Value),
                    PLNAmount = decimal.Parse(p14_2WValue)
                });
            }

            return summaries;
        }

        private DateTime ParseDate(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return DateTime.Now;

            if (DateTime.TryParse(dateString, out var result))
                return result;

            return DateTime.Now;
        }

        private Party MapParty(XElement partyElement)
        {
            if (partyElement == null) return null;

            return new Party
            {
                Eori = partyElement.Descendants(ns + "NrEORI").FirstOrDefault()?.Value,
                Nip = partyElement.Descendants(ns + "DaneIdentyfikacyjne").Descendants(ns + "NIP").FirstOrDefault()?.Value,
                Name = partyElement.Descendants(ns + "DaneIdentyfikacyjne").Descendants(ns + "Nazwa").FirstOrDefault()?.Value,
                MainAddress = new Address
                {
                    CountryCode = partyElement.Descendants(ns + "Adres").Descendants(ns + "KodKraju").FirstOrDefault()?.Value,
                    Line1 = partyElement.Descendants(ns + "Adres").Descendants(ns + "AdresL1").FirstOrDefault()?.Value,
                    GLN = partyElement.Descendants(ns + "Adres").Descendants(ns + "GLN").FirstOrDefault()?.Value
                },
                CorrespondenceAddress = new Address
                {
                    CountryCode = partyElement.Descendants(ns + "AdresKoresp").Descendants(ns + "KodKraju").FirstOrDefault()?.Value,
                    Line1 = partyElement.Descendants(ns + "AdresKoresp").Descendants(ns + "AdresL1").FirstOrDefault()?.Value,
                    GLN = partyElement.Descendants(ns + "AdresKoresp").Descendants(ns + "GLN").FirstOrDefault()?.Value
                },
                Contact = new ContactInfo
                {
                    Email = partyElement.Descendants(ns + "DaneKontaktowe").Descendants(ns + "Email").FirstOrDefault()?.Value,
                    Phone = partyElement.Descendants(ns + "DaneKontaktowe").Descendants(ns + "Telefon").FirstOrDefault()?.Value
                },
                CustomerNumber = partyElement.Descendants(ns + "NrKlienta").FirstOrDefault()?.Value
            };
        }

        private PaymentInfo MapPayment(XElement paymentElement)
        {
            if (paymentElement == null) return null;

            return new PaymentInfo
            {
                IsPartial = paymentElement.Descendants(ns + "ZnacznikZaplatyCzesciowej").FirstOrDefault()?.Value == "1",
                PartialPayments = paymentElement.Descendants(ns + "ZaplataCzesciowa")
                    .Select(p => new PartialPayment
                    {
                        Date = ParseDate(p.Descendants(ns + "DataZaplatyCzesciowej").FirstOrDefault()?.Value),
                        Amount = decimal.Parse(p.Descendants(ns + "KwotaZaplatyCzesciowej").FirstOrDefault()?.Value ?? "0"),
                        Method = p.Descendants(ns + "FormaPlatnosci").FirstOrDefault()?.Value
                    }).ToList()
            };
        }

        private Settlement MapSettlement(XElement settlementElement)
        {
            if (settlementElement == null) return null;

            return new Settlement
            {
                TotalToPay = decimal.Parse(settlementElement.Descendants(ns + "DoZaplaty").FirstOrDefault()?.Value ?? "0")
            };
        }

        private BankAccount MapBankAccount(XElement bankAccountElement)
        {
            if (bankAccountElement == null) return null;

            return new BankAccount
            {
                FullNumber = bankAccountElement.Descendants(ns + "NrRB").FirstOrDefault()?.Value,
                Swift = bankAccountElement.Descendants(ns + "SWIFT").FirstOrDefault()?.Value,
                BankName = bankAccountElement.Descendants(ns + "NazwaBanku").FirstOrDefault()?.Value,
                Description = bankAccountElement.Descendants(ns + "OpisRachunku").FirstOrDefault()?.Value,
                IsBankOwnAccount = int.Parse(bankAccountElement.Descendants(ns + "RachunekWlasnyBanku").FirstOrDefault()?.Value ?? "0")
            };
        }

        private Terms MapTerms(XElement termsElement)
        {
            if (termsElement == null) return null;

            return new Terms
            {
                DeliveryTerms = termsElement.Descendants(ns + "WarunkiDostawy").FirstOrDefault()?.Value
            };
        }
    }
}