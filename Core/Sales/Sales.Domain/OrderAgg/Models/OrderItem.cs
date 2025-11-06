using Sales.Domain.Common;
using Sales.Domain.Common.Base;

namespace Sales.Domain.OrderAgg.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        public ProductItemId ProductItemId { get; init; }
        public int Quantity { get; init; }
        public Order Order { get; set; }

        private OrderItem()
        {

        }

        public OrderItem(ProductItemId productItemId, int quantity)
        {
            ProductItemId = productItemId;
            Quantity = quantity;
        }
    }
}
