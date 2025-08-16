namespace Application.Helpers
{
    public class PageInfo
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public long TotalElements { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }

        public PageInfo(int currentPage, int pageSize, long totalElements)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalElements = totalElements;
            TotalPages = (int)Math.Ceiling((double)totalElements / pageSize);
            HasNext = currentPage < TotalPages - 1;
            HasPrevious = currentPage > 0;
        }
    }
}
