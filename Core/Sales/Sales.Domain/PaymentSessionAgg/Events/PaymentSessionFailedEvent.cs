using Sales.Domain.Common.Base;
using Sales.Domain.PaymentSessionAgg.Models;

namespace Sales.Domain.PaymentSessionAgg.Events
{
    public class PaymentSessionFailedEvent : IDomainEvent
    {
        public PaymentSessionId SessionId { get; init; }

        internal PaymentSessionFailedEvent(PaymentSessionId sessionId)
        {
            SessionId = sessionId;
        }
    }
}