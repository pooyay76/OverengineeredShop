namespace Sales.Application.DTOs
{
    public class QueryCollectionResponse<T>
    {
        public IReadOnlyCollection<T> Result { get; init; } = [];

        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }

        public QueryCollectionResponse(IReadOnlyCollection<T> result, int pageSize, int totalPages, int currentPage, int totalItems)
        {
            Result = result;
            PageSize = pageSize;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            TotalItems = totalItems;
        }
    }
}
