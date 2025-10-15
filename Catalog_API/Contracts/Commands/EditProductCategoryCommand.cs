namespace Catalog_API.Contracts.Commands
{
    public class EditProductCategoryCommand
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
