using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.PaymentSessionAgg.Events
{
    public record PaymentSessionInitiatedEvent : EventBase
    {
        public PaymentSessionId SessionId { get; init; }

        internal PaymentSessionInitiatedEvent(PaymentSessionId sessionId)
        {
            SessionId = sessionId;
        }
    }
}