namespace Ksef_API_ASP.Net.Models
{
    public class FaWierszDTO
    {
        public int NrWiersza { get; set; }
        public decimal KursWaluty { get; set; }
        public required string NazwaUslugi { get; set; } //nazwa usługi
        public decimal Miara{ get; set; } //miara
        public decimal Ilosc { get; set; } //Ilość
        public decimal CenaJednostkowa { get; set; } //cena Jednostkowa
        public decimal WartoscSprzedazyNetto { get; set; } //wartosc sprzedazy netto
        public decimal WartoscPodatkuVat { get; set; } //wartosc podatku vat
    }
}
