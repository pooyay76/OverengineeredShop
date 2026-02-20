using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.OrderAgg.Events
{
    public record NewOrderPlacedEvent : EventBase
    {
        public NewOrderPlacedEvent(OrderId id)
        {
            Id = id;
        }

        public OrderId Id { get; private init; }
    }
}
