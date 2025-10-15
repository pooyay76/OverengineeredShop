using Sales.Domain._common.Base;
using Sales.Domain.OrderAgg.Models;

namespace Sales.Domain.OrderAgg.Events
{
    public record NewOrderPlacedEvent : DomainEvent<OrderId>
    {
        internal NewOrderPlacedEvent(OrderId id) : base(id)
        {
        }
    }
}
