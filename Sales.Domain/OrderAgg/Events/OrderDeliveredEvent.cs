using Sales.Domain.Common.Base;
using Sales.Domain.OrderAgg.Models;

namespace Sales.Domain.OrderAgg.Events
{
    public record OrderDeliveredEvent : DomainEvent<OrderId>
    {
        public OrderDeliveredEvent(OrderId id) : base(id)
        {
        }
    }
}
