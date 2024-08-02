namespace QuranApp.Model
{
    public class Pagination
    {
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Pagination()
        {
        }
        public Pagination(int perPage, int currentPage,
            int? nextPage, int totalPages, int totalRecords)
        {
            PerPage = perPage;
            CurrentPage = currentPage;
            NextPage = nextPage;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}