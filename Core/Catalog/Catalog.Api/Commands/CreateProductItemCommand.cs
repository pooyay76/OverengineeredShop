using Catalog.Api.Models;
using Catalog.Api.Models.ValueObjects;

namespace Catalog.Api.Commands
{
    public class CreateProductItemCommand
    {
        public long ProductId { get; set; }
        public Money Price { get; set; }
        public Dictionary<string, string> ItemFeatures { get; set; } = [];

    }
}
