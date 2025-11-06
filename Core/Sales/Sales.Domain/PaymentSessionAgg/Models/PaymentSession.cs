using Sales.Domain.Common.Base;
using Sales.Domain.Common.ValueObjects;
using Sales.Domain.PaymentSessionAgg.Events;
namespace Sales.Domain.PaymentSessionAgg.Models
{
    public class PaymentSession : AggregateRoot<PaymentSessionId>
    {


        public long SessionId { get; private set; }
        public DateTime TransactionStartDate { get; init; }
        public DateTime TransactionEndDate { get; private set; }
        public string PaymentGatewayUrl { get; private set; }

        /// <summary>
        /// without discount
        /// </summary>
        public Money BaseAmount { get; private set; }

        /// <summary>
        /// with discounts
        /// </summary>
        public Money Amount { get; set; }

        public PaymentTransactionStatus SessionStatus { get; private set; }

        internal PaymentSession(long transactionId, Money amount, string paymentGatewayUrl)
        {
            SessionId = transactionId;
            TransactionStartDate = DateTime.UtcNow;
            Amount = amount;
            SessionStatus = PaymentTransactionStatus.Open;

            AddDomainEvent(new PaymentSessionInitiatedEvent(Id));
            PaymentGatewayUrl = paymentGatewayUrl;
        }
        internal void MarkAsFailed()
        {
            TransactionEndDate = DateTime.UtcNow;
            SessionStatus = PaymentTransactionStatus.Failed;
            AddDomainEvent(new PaymentSessionFailedEvent(Id));

        }

        internal void MarkAsSucceeded()
        {
            TransactionEndDate = DateTime.UtcNow;
            SessionStatus = PaymentTransactionStatus.Successful;
            AddDomainEvent(new PaymentSessionSucceededEvent(Id));

        }
        internal void MarkAsTimedOut()
        {
            TransactionEndDate = DateTime.UtcNow;
            SessionStatus = PaymentTransactionStatus.TimedOut;
            AddDomainEvent(new PaymentSessionSucceededEvent(Id));

        }
        private PaymentSession()
        {

        }
    }

    public enum PaymentTransactionStatus
    {
        Open,
        Failed,
        TimedOut,
        Successful
    }
}
