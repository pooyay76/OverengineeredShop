
using Common.Domain.Language.Catalog.ValueObjects;

namespace Catalog.Api.Commands
{
    public class EditProductCommand
    {
        public ProductId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureMediaAddress { get; set; }
    }
}
