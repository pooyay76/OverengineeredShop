using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductNameChangedEvent : EventBase
    {
        public ProductId ProductId { get; init; }

        public string Name { get; init; }

        public ProductNameChangedEvent(ProductId productId, string name)
        {
            ProductId = productId;
            Name = name;
        }
    }


}
