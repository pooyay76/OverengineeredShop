namespace Catalog.Api.Commands
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ProductPicture { get; set; }
    }
}
