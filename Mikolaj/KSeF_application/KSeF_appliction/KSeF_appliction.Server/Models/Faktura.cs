namespace KSeF_appliction.Server.Models
{
    public class Faktura
    {
        public int Id { get; set; }
        public int Podmiot1Id { get; set; }
        public int Podmiot2Id { get; set; }
        public Podmiot podmiot1 { get; set; } //podmiot 1
        public Podmiot podmiot2 { get; set; } //podmiot 2
        public string Kod_waluty { get; set; } //kod waluty
        public DateTime P_1 { get; set; }//data wystawienia
        public string P_2 { get; set; } //numer faktury
        public DateTime P_6_Od { get; set; } //od
        public DateTime P_6_Do { get; set; } //do
        public decimal P_13_1 { get; set; } //kwota netto
        public decimal P_14_1 { get; set; } //kwota podatku
        public decimal P_14_W { get; set; } //kwota podatku PLN
        public decimal P_15 { get; set; } //kwota do zaplaty/naleznosc
        public List<FaWiersz> Wiersze { get; set; } //pozycje na fakturze
    }
}
