namespace Catalog.Api.Queries
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public long? CategoryId { get; set; }
        public string PictureUrl { get; set; }
        public string Price { get; set; }
    }
}
