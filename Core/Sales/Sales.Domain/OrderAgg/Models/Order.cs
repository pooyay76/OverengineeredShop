using Common.Domain.Base;
using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;
using Sales.Domain.BillAgg.Models;

using Sales.Domain.OrderAgg.Events;

namespace Sales.Domain.OrderAgg.Models
{
    public class Order : AggregateRootBase<OrderId>
    {
        internal Order(UserId customerId, BillId billId, Money amount, List<ProductItemQuantity> productItems, bool isEverythingInStock)
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
            AddEvent(new NewOrderPlacedEvent(Id));
        }
        public UserId CustomerId { get; init; }
        public Money OrderTotal { get; init; }
        public bool IsInStock { get; private set; }
        public DateTime CreationDateTime { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public List<OrderItem> OrderItems { get; init; }
        public Bill Bill { get; init; }
        public BillId BillId { get; init; }

        internal void MarkAsRefunded()
        {

            AddEvent(new OrderMarkedAsRefundedEvent(Id));

        }
        internal void MarkAsShipped()
        {
            AddEvent(new OrderShippedEvent(Id));

        }
        internal void MarkAsDelivered()
        {
            AddEvent(new OrderDeliveredEvent(Id));

        }

        private Order()
        { }
    }
}
