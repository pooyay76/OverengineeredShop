using Common.Domain.Language.Catalog.ValueObjects;

namespace Catalog.Api.Queries
{
    public class ProductCategoryDTO
    {
        public ProductCategoryId Id { get; set; }
        public string Title { get; set; }
        public int ProductsCount { get; set; }
    }
}
