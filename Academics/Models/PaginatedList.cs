namespace Academics.Models
{
    public class PaginatedList<T> where T : class
    {
        public IEnumerable<T>? Entity { get; set; }

        public string NameSortOrder { get; set; }
        public string EmailSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
       

    }
}
