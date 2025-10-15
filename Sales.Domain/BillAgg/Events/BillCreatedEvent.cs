
using Sales.Domain._common.Base;
using Sales.Domain.BillAgg.Models;

namespace Sales.Domain.BillAgg.Events
{
    public record BillCreatedEvent : DomainEvent<BillId>
    {
        public BillCreatedEvent(BillId id) : base(id)
        {
        }

    }
}
