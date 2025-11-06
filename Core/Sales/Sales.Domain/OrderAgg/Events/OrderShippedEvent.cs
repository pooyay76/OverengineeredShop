using Sales.Domain.Common.Base;
using Sales.Domain.OrderAgg.Models;

namespace Sales.Domain.OrderAgg.Events
{
    public record OrderShippedEvent : DomainEvent<OrderId>
    {
        public OrderShippedEvent(OrderId id) : base(id)
        {
        }
    }
}
