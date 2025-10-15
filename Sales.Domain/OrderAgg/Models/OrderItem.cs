using Sales.Domain._common;
using Sales.Domain._common.Base;

namespace Sales.Domain.OrderAgg.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        public ProductItemId ProductItemId { get; init; }
        public int Quantity { get; init; }



        public OrderItem(ProductItemId productItemId, int quantity)
        {
            ProductItemId = productItemId;
            Quantity = quantity;
        }
    }
}
