namespace Catalog_API.Contracts.Queries
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDraftMode { get; set; }
        public long? CategoryId { get; set; }
    }
}
