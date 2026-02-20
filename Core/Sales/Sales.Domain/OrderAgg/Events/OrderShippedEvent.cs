using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.OrderAgg.Events
{
    public record OrderShippedEvent : EventBase
    {
        public OrderShippedEvent(OrderId id) 
        {
            Id = id;
        }

        public OrderId Id { get; private init; }
    }
}
