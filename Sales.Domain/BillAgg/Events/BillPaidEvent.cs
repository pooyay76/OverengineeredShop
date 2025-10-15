using Sales.Domain._common;
using Sales.Domain._common.Base;
using Sales.Domain._common.ValueObjects;
using Sales.Domain.BillAgg.Models;

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
            CustomerId = CustomerId;
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
