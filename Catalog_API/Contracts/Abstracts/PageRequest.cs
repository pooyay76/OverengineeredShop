namespace Catalog.Api.Contracts.Abstracts
{
    public abstract class PageRequest
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

    }
}
