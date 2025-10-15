using Sales.Domain._common.Base;
using Sales.Domain.PaymentSessionAgg.Models;

namespace Sales.Domain.PaymentSessionAgg.Events
{
    public class PaymentSessionTimedOutEvent : IDomainEvent
    {
        public PaymentSessionId SessionId { get; init; }

        internal PaymentSessionTimedOutEvent(PaymentSessionId sessionId)
        {
            SessionId = sessionId;
        }
    }
}