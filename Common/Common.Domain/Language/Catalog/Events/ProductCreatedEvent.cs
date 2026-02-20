using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductCreatedEvent :EventBase
    {
        public ProductId ProductId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string PictureMediaAddress { get; init; }

        public ProductCreatedEvent(ProductId productId, string name,
            string description, string pictureMediaLocation)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            PictureMediaAddress = pictureMediaLocation;
        }
    }
}
