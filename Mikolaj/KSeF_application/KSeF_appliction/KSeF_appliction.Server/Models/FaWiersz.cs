namespace KSeF_appliction.Server.Models
{
    public class FaWiersz
    {
        public int Id { get; set; }
        public int FakturaId { get; set; }
        public int Nr_wiersza { get; set; }
        public string P_7 { get; set; } //nazwa uslugi 
        public decimal P_8A { get; set; } //miara
        public decimal P_8B { get; set; } //miara
        public decimal P_9A { get; set; } //cen. jedn. netto
        public decimal P_11 { get; set; } //wartosc sprzedazy netto
        public decimal P_12 { get; set; } //wartosc podatku vat
        public decimal Kurs_waluty { get; set; } //kurs waluty
    }
}
