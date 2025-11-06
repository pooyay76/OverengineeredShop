namespace Warehouse.Domain.Common.ValueObjects
{
    public record ProductItemQuantity
    {
        public ProductItemId ProductItemId { get; set; }
        public int Quantity { get; set; }
    }
}
