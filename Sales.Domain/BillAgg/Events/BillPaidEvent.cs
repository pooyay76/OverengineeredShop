using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.Common.Base;
using Sales.Domain.Common.ValueObjects;

namespace Sales.Domain.BillAgg.Events
{
    public record BillPaidEvent : DomainEvent<BillId>
    {
        public List<ProductItemQuantity> PurchasedItems { get; init; }
        public DateTime PaidAt { get; init; }
        public Money Amount { get; init; }
        public CustomerId CustomerId { get; init; }
        public BillPaidEvent(CustomerId CustomerId, BillId billId, List<BillItem> billItems,
            Money amount, DateTime paidAt) : base(billId)
        {
            this.CustomerId = CustomerId;
            PurchasedItems = billItems.Select(x => new ProductItemQuantity()
            {
                ProductItemId = x.ProductItemId,
                Quantity = x.Quantity
            }).ToList();
            Amount = amount;
            PaidAt = paidAt;
        }
    }
}
