using Common.Domain.Base;
using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;

namespace Common.Domain.Language.Sales.Events.Global
{
    public record BillPaidEvent : IntegrationEventBase
    {
        public BillId AggregateId { get; private init; }
        public List<ProductItemQuantity> PurchasedItems { get; private init; }
        public DateTime PaidAt { get; private init; }
        public Money Amount { get; private init; }
        public UserId CustomerId { get; private init; }


        public BillPaidEvent(BillId id, UserId CustomerId, List<ProductItemQuantity> billItems,
            Money amount, DateTime paidAt)
        { 
            this.CustomerId = CustomerId;
            PurchasedItems = billItems.Select(x => new ProductItemQuantity()
            {
                ProductItemId = x.ProductItemId,
                Quantity = x.Quantity
            }).ToList();
            Amount = amount;
            PaidAt = paidAt;
            AggregateId = id;
        }
    }
}