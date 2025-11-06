using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.Common.Base;
using Sales.Domain.Common.ValueObjects;
using Sales.Domain.OrderAgg.Events;

namespace Sales.Domain.OrderAgg.Models
{
    public class Order : AggregateRoot<OrderId>
    {
        internal Order(CustomerId customerId, BillId billId, Money amount, List<ProductItemQuantity> productItems, bool isEverythingInStock)
        {

            BillId = billId;
            OrderTotal = amount;
            OrderItems = productItems.Select(x => new OrderItem(x.ProductItemId, x.Quantity))
                .ToList();
            CustomerId = customerId;
            if (isEverythingInStock)
                OrderStatus = OrderStatus.Confirmed;
            else
                OrderStatus = OrderStatus.AwaitingConfirmation;
            CreationDateTime = DateTime.UtcNow;
            IsInStock = isEverythingInStock;
            AddDomainEvent(new NewOrderPlacedEvent(Id));
        }
        public CustomerId CustomerId { get; init; }
        public Money OrderTotal { get; init; }
        public bool IsInStock { get; private set; }
        public DateTime CreationDateTime { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public List<OrderItem> OrderItems { get; init; }
        public Bill Bill { get; init; }
        public BillId BillId { get; init; }

        internal void MarkAsRefunded()
        {

            AddDomainEvent(new OrderMarkedAsRefundedEvent(Id));

        }
        internal void MarkAsShipped()
        {
            AddDomainEvent(new OrderShippedEvent(Id));

        }
        internal void MarkAsDelivered()
        {
            AddDomainEvent(new OrderDeliveredEvent(Id));

        }

        private Order()
        { }
    }
}
