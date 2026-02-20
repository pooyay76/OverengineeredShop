using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;

namespace Common.Domain.Language.Catalog.Events.Global
{
    public record NewPriceAddedEvent : IntegrationEventBase
    {
        public ProductItemId ProductItemId { get;private init; }
        public Money Price { get;private init; }


        public NewPriceAddedEvent(ProductItemId productItemId,Money price)
        {
            ProductItemId = productItemId;
            Price = price;
        }


    }
}
