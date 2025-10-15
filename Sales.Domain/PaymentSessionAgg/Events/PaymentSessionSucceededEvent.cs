using Sales.Domain._common.Base;
using Sales.Domain.PaymentSessionAgg.Models;

namespace Sales.Domain.PaymentSessionAgg.Events
{
    public class PaymentSessionSucceededEvent : IDomainEvent
    {
        public PaymentSessionId SessionId { get; init; }

        internal PaymentSessionSucceededEvent(PaymentSessionId sessionId)
        {
            SessionId = sessionId;
        }
    }
}