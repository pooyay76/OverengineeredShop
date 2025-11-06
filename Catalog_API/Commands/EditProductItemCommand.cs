using Catalog.Api.Models.ValueObjects;

namespace Catalog.Api.Commands
{
    public class EditProductItemCommand
    {
        public long ProductItemId { get;private set; }
        public Money Price { get; private set; }
        public Dictionary<string, string> ItemFeatures { get; private set; } = [];

    }
}
