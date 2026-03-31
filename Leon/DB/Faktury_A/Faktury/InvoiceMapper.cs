using Faktury.models;
using System.Xml.Linq;

namespace Faktury
{
    public class InvoiceMapper
    {
        private static readonly XNamespace ns = "http://crd.gov.pl/wzor/2025/06/25/13775/";

        public Invoice MapXmlToInvoice(string xmlFilePath)
        {
            var doc = XDocument.Load(xmlFilePath);

            var faktura = doc.Root;
            if (faktura == null) return null;

            var fa = faktura.Element(ns + "Fa");
            if (fa == null) return null;

            var invoice = new Invoice
            {
                InvoiceNumber = fa.Element(ns + "P_2")?.Value,
                KsefNumber = faktura.Element(ns + "Naglowek")?.Element(ns + "KodFormularza")?.Value,
                IssueDate = ParseDate(fa.Element(ns + "P_1")?.Value),
                DeliveryDate = ParseDate(fa.Element(ns + "P_6")?.Value),
                IssuePlace = fa.Element(ns + "P_1M")?.Value,
                CurrencyCode = fa.Element(ns + "KodWaluty")?.Value,
                CurrencyRate = decimal.Parse(fa.Element(ns + "KursWalutyZ")?.Value ?? "1"),

                Seller = MapParty(faktura.Element(ns + "Podmiot1")),
                Buyer = MapParty(faktura.Element(ns + "Podmiot2")),
                OtherParties = faktura.Elements(ns + "Podmiot3").Select(MapParty).ToList(),

                Lines = fa.Elements(ns + "ZamowienieWiersz").Select(MapLine).ToList(),
                TaxSummaries = MapTaxSummaries(fa),

                Payment = MapPayment(fa.Element(ns + "Platnosc")),
                Settlement = MapSettlement(fa.Element(ns + "Rozliczenie")),

                SellerBankAccount = MapBankAccount(fa.Element(ns + "RachunekBankowy")),
                FactorBankAccount = MapBankAccount(fa.Element(ns + "RachunekBankowyFaktora")),

                TransactionTerms = MapTerms(fa.Element(ns + "WarunkiTransakcji")),
                FooterNote = faktura.Descendants(ns + "StopkaFaktury").FirstOrDefault()?.Value
            };

            return invoice;
        }


        private Party MapParty(XElement p)
        {
            if (p == null) return null;

            return new Party
            {
                Role = p.Element(ns + "Rola")?.Value,
                Eori = p.Element(ns + "NrEORI")?.Value,
                Nip = p.Element(ns + "DaneIdentyfikacyjne")?.Element(ns + "NIP")?.Value,
                Name = p.Element(ns + "DaneIdentyfikacyjne")?.Element(ns + "Nazwa")?.Value,
                CustomerNumber = p.Element(ns + "NrKlienta")?.Value,

                MainAddress = MapAddress(p.Element(ns + "Adres")),
                CorrespondenceAddress = MapAddress(p.Element(ns + "AdresKoresp")),
                Contact = MapContact(p.Element(ns + "DaneKontaktowe"))
            };
        }

        private ContactInfo MapContact(XElement c)
        {
            if (c == null) return null;

            return new ContactInfo
            {
                Email = c.Element(ns + "Email")?.Value,
                Phone = c.Element(ns + "Telefon")?.Value
            };
        }

        private Address MapAddress(XElement a)
        {
            if (a == null) return null;

            return new Address
            {
                CountryCode = a.Element(ns + "KodKraju")?.Value,
                Line1 = a.Element(ns + "AdresL1")?.Value,
                Line2 = a.Element(ns + "AdresL2")?.Value,
                GLN = a.Element(ns + "GLN")?.Value
            };
        }


        private InvoiceLine MapLine(XElement x)
        {
            return new InvoiceLine
            {
                Name = x.Element(ns + "P_7Z")?.Value,
                PricePerPiceNetto = decimal.Parse(x.Element(ns + "P_11NettoZ")?.Value ?? "0"),
                Quantity = int.Parse(x.Element(ns + "P_8BZ")?.Value ?? "0"),
                Unit = x.Element(ns + "P_8AZ")?.Value,
                TaxRate = int.Parse(x.Element(ns + "P_12Z")?.Value ?? "0"),
                PriceTotalNetto = x.Element(ns + "P_11NettoZ")?.Value,
                TaxValue = decimal.Parse(x.Element(ns + "P_11VatZ")?.Value ?? "0")
            };
        }


        private List<TaxSummary> MapTaxSummaries(XElement fa)
        {
            var list = new List<TaxSummary>();

            var rates = fa.Elements(ns + "P_13_1").ToList();
            var netto = fa.Elements(ns + "P_13_2").ToList();
            var tax = fa.Elements(ns + "P_14_1").ToList();
            var brutto = fa.Elements(ns + "P_15").ToList();
            var pln = fa.Elements(ns + "P_14_2W").ToList();

            for (int i = 0; i < rates.Count; i++)
            {
                list.Add(new TaxSummary
                {
                    TaxRate = rates[i].Value,
                    Netto = decimal.Parse(netto.ElementAtOrDefault(i)?.Value ?? "0"),
                    TaxAmount = decimal.Parse(tax.ElementAtOrDefault(i)?.Value ?? "0"),
                    Brutto = decimal.Parse(brutto.ElementAtOrDefault(i)?.Value ?? "0"),
                    PLNAmount = decimal.Parse(pln.ElementAtOrDefault(i)?.Value ?? "0")
                });
            }

            return list;
        }


        private PaymentInfo MapPayment(XElement p)
        {
            if (p == null) return null;

            var pay = new PaymentInfo
            {
                IsPartial = p.Element(ns + "ZnacznikZaplatyCzesciowej")?.Value == "1",
                PaymentMethod = p.Element(ns + "FormaPlatnosci")?.Value
            };

            var term = p.Element(ns + "TerminPlatnosci");
            if (term != null)
            {
                pay.PaymentDueDate = ParseDate(term.Element(ns + "Termin")?.Value);

                var desc = term.Element(ns + "TerminOpis");
                if (desc != null)
                {
                    pay.PaymentTermsDescription =
                        $"{desc.Element(ns + "Ilosc")?.Value} {desc.Element(ns + "Jednostka")?.Value} - {desc.Element(ns + "ZdarzeniePoczatkowe")?.Value}";
                }
            }

            pay.PartialPayments = p.Elements(ns + "ZaplataCzesciowa")
                .Select(z => new PartialPayment
                {
                    Date = ParseDate(z.Element(ns + "DataZaplatyCzesciowej")?.Value),
                    Amount = decimal.Parse(z.Element(ns + "KwotaZaplatyCzesciowej")?.Value ?? "0"),
                    Method = z.Element(ns + "FormaPlatnosci")?.Value
                })
                .ToList();

            return pay;
        }


        private Settlement MapSettlement(XElement s)
        {
            if (s == null) return null;

            var set = new Settlement
            {
                TotalToPay = decimal.Parse(s.Element(ns + "DoZaplaty")?.Value ?? "0")
            };

            set.Charges = s.Elements(ns + "Obciazenia")
                .Select(c => new Charge
                {
                    Amount = decimal.Parse(c.Element(ns + "Kwota")?.Value ?? "0"),
                    Reason = c.Element(ns + "Powod")?.Value
                })
                .ToList();

            set.Deductions = s.Elements(ns + "Odliczenia")
                .Select(d => new Deduction
                {
                    Amount = decimal.Parse(d.Element(ns + "Kwota")?.Value ?? "0"),
                    Reason = d.Element(ns + "Powod")?.Value
                })
                .ToList();

            return set;
        }


        private BankAccount MapBankAccount(XElement b)
        {
            if (b == null) return null;

            return new BankAccount
            {
                FullNumber = b.Element(ns + "NrRB")?.Value,
                Swift = b.Element(ns + "SWIFT")?.Value,
                BankName = b.Element(ns + "NazwaBanku")?.Value,
                Description = b.Element(ns + "OpisRachunku")?.Value,
                IsBankOwnAccount = int.Parse(b.Element(ns + "RachunekWlasnyBanku")?.Value ?? "0")
            };
        }


        private Terms MapTerms(XElement t)
        {
            if (t == null) return null;

            var terms = new Terms
            {
                DeliveryTerms = t.Element(ns + "WarunkiDostawy")?.Value
            };

            var contract = t.Element(ns + "Umowy");
            if (contract != null)
            {
                terms.Contract = new Contract
                {
                    ContractDate = ParseDate(contract.Element(ns + "DataUmowy")?.Value),
                    ContractNumber = contract.Element(ns + "NrUmowy")?.Value
                };
            }

            var order = t.Element(ns + "Zamowienia");
            if (order != null)
            {
                terms.Order = new OrderInfo
                {
                    OrderDate = ParseDate(order.Element(ns + "DataZamowienia")?.Value),
                    OrderNumber = order.Element(ns + "NrZamowienia")?.Value
                };
            }

            var transport = t.Element(ns + "Transport");
            if (transport != null)
            {
                terms.Transport = MapTransport(transport);
            }

            return terms;
        }


        private TransportInfo MapTransport(XElement t)
        {
            var info = new TransportInfo
            {
                TransportType = int.Parse(t.Element(ns + "RodzajTransportu")?.Value ?? "0"),
                TransportOrderNumber = t.Element(ns + "NrZleceniaTransportu")?.Value,
                CargoDescription = int.Parse(t.Element(ns + "OpisLadunku")?.Value ?? "0"),
                PackagingUnit = t.Element(ns + "JednostkaOpakowania")?.Value,
                StartDate = ParseDate(t.Element(ns + "DataGodzRozpTransportu")?.Value),
                EndDate = ParseDate(t.Element(ns + "DataGodzZakTransportu")?.Value),

                Carrier = MapCarrier(t.Element(ns + "Przewoznik")),
                ShipFrom = MapAddress(t.Element(ns + "WysylkaZ")),
                ShipVia = MapAddress(t.Element(ns + "WysylkaPrzez")),
                ShipTo = MapAddress(t.Element(ns + "WysylkaDo"))
            };

            return info;
        }

        private Carrier MapCarrier(XElement c)
        {
            if (c == null) return null;

            return new Carrier
            {
                CountryCode = c.Element(ns + "DaneIdentyfikacyjne")?.Element(ns + "KodKraju")?.Value,
                TaxId = c.Element(ns + "DaneIdentyfikacyjne")?.Element(ns + "NrID")?.Value,
                Name = c.Element(ns + "DaneIdentyfikacyjne")?.Element(ns + "Nazwa")?.Value,
                Address = MapAddress(c.Element(ns + "AdresPrzewoznika"))
            };
        }


        private DateTime ParseDate(string s)
        {
            if (DateTime.TryParse(s, out var d))
                return d;

            return DateTime.MinValue;
        }
    }
}