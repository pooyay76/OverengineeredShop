namespace Catalog.Api.Commands
{
    public class AssignProductToProductCategoryCommand
    {
        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }
    }
}
