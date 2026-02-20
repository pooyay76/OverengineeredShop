
namespace Catalog.Api.Commands
{
    public class EditProductItemCommand
    {
        public long ProductItemId { get;private set; }
        public decimal Price { get; private set; }
        public Dictionary<string, string> ItemFeatures { get; private set; } = [];

    }
}
