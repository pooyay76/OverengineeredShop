using Catalog.Api.Models.ValueObjects;

namespace Catalog.Api.Commands
{
    public class EditProductCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
