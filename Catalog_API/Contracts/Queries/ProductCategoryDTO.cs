namespace Catalog_API.Contracts.Queries
{
    public class ProductCategoryDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int ProductsCount { get; set; }
    }
}
