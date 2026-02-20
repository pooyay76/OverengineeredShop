using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.OrderAgg.Events
{
    public record OrderMarkedAsRefundedEvent : EventBase
    {
        public OrderMarkedAsRefundedEvent(OrderId id) 
        {
            Id = id;
        }

        public OrderId Id { get; private init; }
    }
}
