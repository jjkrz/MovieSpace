namespace Application.Helpers
{
    public class PaginatedResponse<T>
    {
        public List<T> Content { get; set; } = new();
        public PageInfo PageInfo { get; set; }
        public PaginatedResponse(List<T> content, PageInfo pageInfo)
        {
            Content = content;
            PageInfo = pageInfo;
        }
    }
}
