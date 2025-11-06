using Sales.Domain.Common.Base;
using Sales.Domain.PaymentSessionAgg.Models;

namespace Sales.Domain.PaymentSessionAgg.Events
{
    public class PaymentSessionInitiatedEvent : IDomainEvent
    {
        public PaymentSessionId SessionId { get; init; }

        internal PaymentSessionInitiatedEvent(PaymentSessionId sessionId)
        {
            SessionId = sessionId;
        }
    }
}