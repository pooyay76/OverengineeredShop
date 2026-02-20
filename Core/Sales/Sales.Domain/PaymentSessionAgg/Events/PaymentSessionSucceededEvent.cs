using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.PaymentSessionAgg.Events
{
    public record PaymentSessionSucceededEvent : EventBase
    {
        public PaymentSessionId SessionId { get; init; }

        internal PaymentSessionSucceededEvent(PaymentSessionId sessionId)
        {
            SessionId = sessionId;
        }
    }
}