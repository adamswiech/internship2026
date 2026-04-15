namespace IndexApi.Models
{
    public class PagedDTO<T>
    {
        public List<T> Items { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
    }
}
