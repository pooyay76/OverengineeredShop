namespace Sales.Domain._common.ValueObjects
{
    public record ProductItemQuantity
    {
        public ProductItemId ProductItemId { get; set; }
        public int Quantity { get; set; }
    }
}
