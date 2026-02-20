using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.PaymentSessionAgg.Events
{
    public record PaymentSessionTimedOutEvent : EventBase
    {
        public PaymentSessionId SessionId { get; init; }

        internal PaymentSessionTimedOutEvent(PaymentSessionId sessionId)
        {
            SessionId = sessionId;
        }
    }
}