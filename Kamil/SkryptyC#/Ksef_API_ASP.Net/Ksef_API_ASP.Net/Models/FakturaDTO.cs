using Ksef.Models;

namespace Ksef_API_ASP.Net.Models
{
    public class FakturaDTO
    {

        public required PodmiotDTO Sprzedawca{ get; set; } // sprzedawca
        public required PodmiotDTO Nabywca { get; set; } // nabywca
        public required string KodWaluty { get; set; }
        public required DateTime DataWyslania { get; set; } 
        public required string NrFaktury { get; set; } 
        public required DateTime DataOd { get; set; } 
        public required DateTime DataDo { get; set; }
        public required decimal KwatoaNetto { get; set; } 
        public required decimal KwotaPodatku { get; set; } 
        public required decimal KwotaPodatkuPLN { get; set; } 
        public required decimal KwotaNaloznosci { get; set; } 
        public required List<FaWierszDTO> FaWiersze { get; set; }
        public List<int>? tmp { get; set; }
        public bool? t { get; set; }
        public object? ob { get; set; }
    }
}
