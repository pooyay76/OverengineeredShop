using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Events
{
    public record BillCreatedEvent : DomainEvent<BillId>
    {
        public BillCreatedEvent(BillId id) : base(id)
        {
        }

    }
}
