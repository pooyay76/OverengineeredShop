using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductPublishedEvent : EventBase
    {
        public ProductId ProductId { get; init; }

        public ProductPublishedEvent(ProductId productId)
        {
            ProductId = productId;
        }
    }
}
