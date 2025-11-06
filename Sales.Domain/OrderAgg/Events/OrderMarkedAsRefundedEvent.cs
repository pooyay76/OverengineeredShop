using Sales.Domain.Common.Base;
using Sales.Domain.OrderAgg.Models;

namespace Sales.Domain.OrderAgg.Events
{
    public record OrderMarkedAsRefundedEvent : DomainEvent<OrderId>
    {
        public OrderMarkedAsRefundedEvent(OrderId id) : base(id)
        {
        }
    }
}
