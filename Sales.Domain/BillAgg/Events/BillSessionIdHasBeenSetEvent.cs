using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Events
{
    public record BillSessionIdHasBeenSetEvent : DomainEvent<BillId>
    {
        public string SessionId { get; init; }
        public BillSessionIdHasBeenSetEvent(BillId id, string sessionId) : base(id)
        {
            SessionId = sessionId;
        }

    }
}