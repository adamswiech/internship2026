namespace AspireApp1.Server.Models
{
    public class faWiersz
    {
        public int id { get; set; }
        public int fakturaId { get; set; }
        public int nrWiersza { get; set; }
        public string p_7 { get; set; }
        public decimal? p_8A { get; set; }
        public decimal? p_8B { get; set; }
        public decimal? p_9A { get; set; }
        public decimal? p_11 { get; set; }
        public decimal? p_12 { get; set; }
        public decimal? kursWaluty { get; set; }

        public virtual faktura faktura { get; set; }
    }
}