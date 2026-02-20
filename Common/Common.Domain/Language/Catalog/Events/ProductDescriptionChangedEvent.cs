using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductDescriptionChangedEvent : EventBase
    {
        public ProductId ProductId{ get; init; }
        public string Description { get; init; }

        public ProductDescriptionChangedEvent(ProductId Id,string description)
        {
            ProductId = Id;
            Description = description;
        }
    }
}
