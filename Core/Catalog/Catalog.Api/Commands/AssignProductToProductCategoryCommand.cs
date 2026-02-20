using Common.Domain.Language.Catalog.ValueObjects;

namespace Catalog.Api.Commands
{
    public class AssignProductToProductCategoryCommand
    {
        public ProductId ProductId { get; set; }
        public ProductCategoryId ProductCategoryId { get; set; }
    }
}
