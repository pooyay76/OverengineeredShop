using Common.Domain.Language.Catalog.ValueObjects;

namespace Catalog.Api.Commands
{
    public class CreateProductItemCommand
    {
        public ProductId ProductId { get; set; }
        public decimal Price { get; set; }
        public Dictionary<string, string> ItemFeatures { get; set; } = [];

    }
}
