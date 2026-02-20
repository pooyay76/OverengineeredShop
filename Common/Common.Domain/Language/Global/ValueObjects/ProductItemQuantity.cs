using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Global.ValueObjects
{
    public record ProductItemQuantity
    {
        public ProductItemId ProductItemId { get; set; }
        public int Quantity { get; set; }
    }
}
