using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.PaymentSessionAgg.Events
{
    public record PaymentSessionFailedEvent : EventBase
    {
        public PaymentSessionId SessionId { get; init; }

        internal PaymentSessionFailedEvent(PaymentSessionId sessionId)
        {
            SessionId = sessionId;
        }
    }
}