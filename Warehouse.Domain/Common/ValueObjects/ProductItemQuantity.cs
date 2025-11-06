using Warehouse.Domain.Common.ValueObjects;

namespace Sales.Domain.Common.ValueObjects
{
    public record ProductItemQuantity
    {
        public ProductItemId ProductItemId { get; set; }
        public int Quantity { get; set; }
    }
}
