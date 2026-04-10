using System.Xml.Serialization;

namespace Ksef_API_ASP.Net.Models
{
    [XmlRoot("Faktura", Namespace = "http://crd.gov.pl/wzor/2025/06/25/13775/")]
    public class KsefFaktura
    {
        public KsefNaglowek Naglowek { get; set; }
        public KsefPodmiot1 Podmiot1 { get; set; }
        public KsefPodmiot2 Podmiot2 { get; set; }
        public KsefFa Fa { get; set; }
    }

    public class KsefNaglowek
    {
        public KsefKodFormularza KodFormularza { get; set; }

        [XmlElement("WariantFormularza")]
        public string WariantFormularza { get; set; } = "3";

        [XmlElement("DataWytworzeniaFa")]
        public DateTime DataWytworzeniaFa { get; set; }

        [XmlElement("SystemInfo")]
        public string SystemInfo { get; set; } = "Aplikacja Podatnika KSeF";
    }

    public class KsefKodFormularza
    {
        [XmlAttribute("kodSystemowy")]
        public string KodSystemowy { get; set; } = "FA (3)";

        [XmlAttribute("wersjaSchemy")]
        public string WersjaSchemy { get; set; } = "1-0E";

        [XmlText]
        public string Value { get; set; } = "FA";
    }

    public class KsefPodmiot1
    {
        [XmlElement("DaneIdentyfikacyjne")]
        public KsefDaneIdentyfikacyjne DaneIdentyfikacyjne { get; set; }

        [XmlElement("Adres")]
        public KsefAdres Adres { get; set; }
    }

    public class KsefPodmiot2
    {
        [XmlElement("DaneIdentyfikacyjne")]
        public KsefDaneIdentyfikacyjne DaneIdentyfikacyjne { get; set; }

        [XmlElement("Adres")]
        public KsefAdres Adres { get; set; }

        [XmlElement("JST")]
        public string JST { get; set; } = "2";

        [XmlElement("GV")]
        public string GV { get; set; } = "2";
    }

    public class KsefDaneIdentyfikacyjne
    {
        [XmlElement("NIP")]
        public string NIP { get; set; }

        [XmlElement("Nazwa")]
        public string Nazwa { get; set; }
    }

    public class KsefAdres
    {
        [XmlElement("KodKraju")]
        public string KodKraju { get; set; }

        [XmlElement("AdresL1")]
        public string AdresL1 { get; set; }
    }

    public class KsefFa
    {
        [XmlElement("KodWaluty")]
        public string KodWaluty { get; set; } = "EUR";

        [XmlElement("P_1")]
        public DateTime P_1 { get; set; }

        [XmlElement("P_2")]
        public string P_2 { get; set; }

        [XmlElement("OkresFa")]
        public KsefOkresFa OkresFa { get; set; }

        [XmlElement("P_13_1")]
        public decimal P_13_1 { get; set; }

        [XmlElement("P_14_1")]
        public decimal P_14_1 { get; set; }

        [XmlElement("P_14_1W")]
        public decimal P_14_1W { get; set; }

        [XmlElement("P_15")]
        public decimal P_15 { get; set; }

        [XmlElement("Adnotacje")]
        public KsefAdnotacje Adnotacje { get; set; }

        [XmlElement("RodzajFaktury")]
        public string RodzajFaktury { get; set; } = "VAT";

        [XmlElement("FaWiersz")]
        public List<KsefFaWiersz> FaWiersz { get; set; } = new List<KsefFaWiersz>();
    }

    public class KsefOkresFa
    {
        [XmlElement("P_6_Od")]
        public DateTime P_6_Od { get; set; }

        [XmlElement("P_6_Do")]
        public DateTime P_6_Do { get; set; }
    }

    public class KsefAdnotacje
    {
        [XmlElement("P_16")]
        public string P_16 { get; set; } = "2";

        [XmlElement("P_17")]
        public string P_17 { get; set; } = "2";

        [XmlElement("P_18")]
        public string P_18 { get; set; } = "2";

        [XmlElement("P_18A")]
        public string P_18A { get; set; } = "2";

        [XmlElement("Zwolnienie")]
        public KsefZwolnienie Zwolnienie { get; set; }

        [XmlElement("NoweSrodkiTransportu")]
        public KsefNoweSrodkiTransportu NoweSrodkiTransportu { get; set; }

        [XmlElement("P_23")]
        public string P_23 { get; set; } = "2";

        [XmlElement("PMarzy")]
        public KsefPMarzy PMarzy { get; set; }
    }

    public class KsefZwolnienie
    {
        [XmlElement("P_19")]
        public string P_19 { get; set; } = "1";

        [XmlElement("P_19A")]
        public string P_19A { get; set; } = "CPKa";
    }

    public class KsefNoweSrodkiTransportu
    {
        [XmlElement("P_22N")]
        public string P_22N { get; set; } = "1";
    }

    public class KsefPMarzy
    {
        [XmlElement("P_PMarzyN")]
        public string P_PMarzyN { get; set; } = "1";
    }

    public class KsefFaWiersz
    {
        [XmlElement("NrWierszaFa")]
        public int NrWierszaFa { get; set; }

        [XmlElement("P_7")]
        public string P_7 { get; set; }

        [XmlElement("P_8A")]
        public decimal P_8A { get; set; }

        [XmlElement("P_8B")]
        public decimal P_8B { get; set; }

        [XmlElement("P_9A")]
        public decimal P_9A { get; set; }

        [XmlElement("P_11")]
        public decimal P_11 { get; set; }

        [XmlElement("P_12")]
        public decimal P_12 { get; set; }

        [XmlElement("KursWaluty")]
        public decimal KursWaluty { get; set; }
    }


}

