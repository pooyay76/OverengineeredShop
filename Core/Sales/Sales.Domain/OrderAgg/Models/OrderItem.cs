using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Domain.OrderAgg.Models
{
    public class OrderItem : EntityBase<OrderItemId>
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
