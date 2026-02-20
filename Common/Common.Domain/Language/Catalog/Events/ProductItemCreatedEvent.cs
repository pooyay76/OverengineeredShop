
using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductItemCreatedEvent : EventBase
    {
        public ProductItemId Id { get; init; }
        public ProductId ProductId { get; init; }
        public Money Price { get; init; }
        public Dictionary<string, string> ItemFeatures { get; init; } = [];

        public ProductItemCreatedEvent(ProductItemId productItemId, ProductId productId, Money price, Dictionary<string, string> itemFeatures)
        {
            Id = productItemId;
            ProductId = productId;
            Price = price;
            ItemFeatures = itemFeatures;
        }
    }
}
