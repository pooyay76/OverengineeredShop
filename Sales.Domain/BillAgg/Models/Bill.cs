#nullable enable
using Sales.Domain._common;
using Sales.Domain._common.Base;
using Sales.Domain._common.ValueObjects;
using Sales.Domain.BillAgg.Constants;
using Sales.Domain.BillAgg.Events;
using Sales.Domain.BillAgg.Exceptions;
using Sales.Domain.DiscountAgg.Models;

namespace Sales.Domain.BillAgg.Models
{
    public class Bill : AggregateRoot<BillId>
    {

        public CustomerId CustomerId { get; private set; }
        public string PaymentSessionId { get; private set; } = "";
        public List<BillItem> BillItems { get; private set; } = [];
        public BillStatus BillStatus { get; private set; } = BillStatus.AwaitingPayment;

        //this will be used later when payment session is introduced
        public DateTime CreationDateTime { get; private set; } = DateTime.UtcNow;
        public DateTime ExpirationDateTime { get; private set; } = DateTime.UtcNow + BillActiveDurationTimeSpanConstant.Value;
        public DateTime PaidAt { get; private set; }



        public Money TotalItemPricesBase { get; private set; } = new Money(0);
        public Money TotalItemsDiscountedAmount { get; private set; } = new Money(0);
        public Money TotalItemsPriceWithDiscount { get; private set; } = new Money(0);

        public Money TotalBillItemsDiscount { get; private set; } = new Money(0);
        public Money TotalTax { get; private set; } = new Money(0);

        public Money ShippingCost { get; private set; } = new Money(0);
        public Money TotalBilling { get; private set; } = new Money(0);
        public Money TotalBillingDiscountedAmount { get; private set; } = new Money(0);

        public DiscountId? BillDiscountId { get; private set; }
        public Discount? BillDiscount { get; private set; }

        internal Bill(CustomerId userId, List<BillItem> items, Money shippingCost,
            Discount? billDiscount = null)
        {
            CustomerId = userId;
            ShippingCost = shippingCost;
            BillDiscount = billDiscount;
            BillItems.AddRange(items);

            TotalItemPricesBase = new Money(items.Sum(x => x.UnitPriceBase.GetValue()));
            TotalItemsPriceWithDiscount = new Money(items.Sum(x => x.UnitPriceWithDiscount.GetValue()));
            TotalItemsDiscountedAmount = new Money(items.Sum(x => x.DiscountedAmount.GetValue()));

            TotalTax = new Money(TaxPercentageAmountConstant.Value * TotalItemsPriceWithDiscount.GetValue());
            if (items == null || items.Count == 0)
            {
                throw new EmptyBillException();
            }
            TotalBilling = TotalItemsPriceWithDiscount + TotalTax + ShippingCost;


            if (BillDiscount != null)
            {
                decimal discountPercentage = BillDiscount.DiscountPercentage;
                if (discountPercentage <= 0 || discountPercentage > 100)
                {
                    throw new ArgumentOutOfRangeException();
                }
                TotalBillingDiscountedAmount = new Money(discountPercentage * TotalBilling.GetValue());

                TotalBilling -= TotalBillingDiscountedAmount;
            }


            BillStatus = BillStatus.AwaitingPayment;

            AddDomainEvent(new BillCreatedEvent(Id));
        }
        internal void SetSession(string sessionId)
        {
            if (BillStatus != BillStatus.AwaitingPayment)
                throw new CannotChangeBillStatusAfterItIsSetException();
            if (DateTime.UtcNow >= ExpirationDateTime)
                throw new BillExpiredException();
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                throw new SessionIdCannotBeEmptyException();
            }
            PaymentSessionId = sessionId;
        }
        internal void MarkAsPaid()
        {
            if (BillStatus != BillStatus.AwaitingPayment)
            {
                throw new CannotChangeBillStatusAfterItIsSetException();
            }
            BillStatus = BillStatus.Paid;
            PaidAt = DateTime.UtcNow;
            AddDomainEvent(new BillPaidEvent(CustomerId, Id, BillItems, TotalBilling, PaidAt));
        }
        internal void MarkAsFailed()
        {

            if (BillStatus != BillStatus.AwaitingPayment)
            {
                throw new CannotChangeBillStatusAfterItIsSetException();
            }

            BillStatus = BillStatus.PaymentFailed;

            AddDomainEvent(new BillPaymentFailedEvent(Id));
        }

    }
}
