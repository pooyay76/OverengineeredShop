using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Common.Domain.Language.Sales.Events
{
    public record BillSessionIdHasBeenSetEvent : EventBase
    {
        public BillId Id { get; private init; }
        public string SessionId { get; init; }
        public BillSessionIdHasBeenSetEvent(BillId id, string sessionId) 
        {
            Id = id;
            SessionId = sessionId;
        }

    }
}