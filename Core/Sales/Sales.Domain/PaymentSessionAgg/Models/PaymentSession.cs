using Common.Domain.Base;
using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;
using Sales.Domain.PaymentSessionAgg.Events;
namespace Sales.Domain.PaymentSessionAgg.Models
{
    public class PaymentSession : AggregateRootBase<PaymentSessionId>
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

            AddEvent(new PaymentSessionInitiatedEvent(Id));
            PaymentGatewayUrl = paymentGatewayUrl;
        }
        internal void MarkAsFailed()
        {
            TransactionEndDate = DateTime.UtcNow;
            SessionStatus = PaymentTransactionStatus.Failed;
            AddEvent(new PaymentSessionFailedEvent(Id));

        }

        internal void MarkAsSucceeded()
        {
            TransactionEndDate = DateTime.UtcNow;
            SessionStatus = PaymentTransactionStatus.Successful;
            AddEvent(new PaymentSessionSucceededEvent(Id));

        }
        internal void MarkAsTimedOut()
        {
            TransactionEndDate = DateTime.UtcNow;
            SessionStatus = PaymentTransactionStatus.TimedOut;
            AddEvent(new PaymentSessionSucceededEvent(Id));

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
