using Common.Domain.Language.Catalog.ValueObjects;

namespace Catalog.Api.Queries
{
    public class ProductDTO
    {
        public ProductId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public ProductCategoryId CategoryId { get; set; }
        public string PictureUrl { get; set; }
        public string Price { get; set; }
        public int ItemsCount { get; internal set; }
    }
}
