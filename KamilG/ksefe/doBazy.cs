using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using ksefe.Data;
using ksefe.Models;

namespace ksefe
{
    internal class doBazy
    {
        private readonly KsefeDbContext db;

        public doBazy(KsefeDbContext db)
        {
            this.db = db;
        }

        public void ImportFromXml(string xmlPath)
        {
            var doc = XDocument.Load(xmlPath);
            var root = doc.Root ?? throw new InvalidOperationException("Brak elementu głównego XML.");
            XNamespace ns = root.Name.Namespace;

            var podmiot1 = GetOrCreatePodmiot(root.Element(ns + "Podmiot1"), ns);
            var podmiot2 = GetOrCreatePodmiot(root.Element(ns + "Podmiot2"), ns);

            db.SaveChanges();

            var faElement = root.Element(ns + "Fa") ?? throw new InvalidOperationException("Brak sekcji Fa w XML.");

            var nowaFaktura = new faktura
            {
                podmiot1Id = podmiot1.id,
                podmiot2Id = podmiot2.id,
                kodWaluty = GetValue(faElement, ns, "KodWaluty"),
                p_1 = ParseDate(GetValue(faElement, ns, "P_1")) ?? DateTime.Now,
                p_2 = GetValue(faElement, ns, "P_2"),
                p_6Od = ParseDate(GetValue(faElement, ns, "P_6")),
                p_6Do = ParseDate(GetValue(faElement, ns, "P_6")),
                p_13_1 = ParseDecimal(GetValue(faElement, ns, "P_13_1")) ?? 0m,
                p_14_1 = ParseDecimal(GetValue(faElement, ns, "P_14_1")) ?? 0m,
                p_14W = ParseDecimal(GetValue(faElement, ns, "P_14_W")) ?? ParseDecimal(GetValue(faElement, ns, "P_14_1")) ?? 0m,
                p_15 = ParseDecimal(GetValue(faElement, ns, "P_15")) ?? 0m
            };

            foreach (var wierszElement in faElement.Elements(ns + "FaWiersz"))
            {
                nowaFaktura.wiersze.Add(new faWiersz
                {
                    nrWiersza = ParseInt(GetValue(wierszElement, ns, "NrWierszaFa")) ?? 0,
                    p_7 = GetValue(wierszElement, ns, "P_7"),
                    p_8A = ParseDecimal(GetValue(wierszElement, ns, "P_8A")),
                    p_8B = ParseDecimal(GetValue(wierszElement, ns, "P_8B")),
                    p_9A = ParseDecimal(GetValue(wierszElement, ns, "P_9A")),
                    p_11 = ParseDecimal(GetValue(wierszElement, ns, "P_11")),
                    p_12 = ParseDecimal(GetValue(wierszElement, ns, "P_12")),
                    kursWaluty = null
                });
            }

            db.Faktury.Add(nowaFaktura);
            db.SaveChanges();
        }

        private podmiot GetOrCreatePodmiot(XElement? podmiotElement, XNamespace ns)
        {
            if (podmiotElement is null)
            {
                return new podmiot();
            }

            var nip = GetValue(podmiotElement.Element(ns + "DaneIdentyfikacyjne"), ns, "NIP");
            var name = GetValue(podmiotElement.Element(ns + "DaneIdentyfikacyjne"), ns, "Nazwa");
            var kod = GetValue(podmiotElement.Element(ns + "Adres"), ns, "KodKraju");
            var adres = GetValue(podmiotElement.Element(ns + "Adres"), ns, "AdresL1");

            var existing = db.Podmioty.FirstOrDefault(p => p.nip == nip && p.name == name);
            if (existing is not null)
            {
                return existing;
            }

            var nowyPodmiot = new podmiot
            {
                nip = nip,
                name = name,
                kod = kod,
                adres = adres
            };

            db.Podmioty.Add(nowyPodmiot);
            return nowyPodmiot;
        }

        private static string GetValue(XElement? parent, XNamespace ns, string elementName)
            => parent?.Element(ns + elementName)?.Value ?? string.Empty;

        private static int? ParseInt(string value)
            => int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;

        private static decimal? ParseDecimal(string value)
            => decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : null;

        private static DateTime? ParseDate(string value)
            => DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var result) ? result : null;
    }
}
