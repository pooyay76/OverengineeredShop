using Sales.Domain._common;
using Sales.Domain._common.ValueObjects;

namespace Sales.Domain.PriceHistoryAgg.Models
{
    public class PriceLabel
    {
        public long Id { get; private set; }
        public ProductItemId ProductItemId { get; private set; }
        public DateTime CreationDateTime { get; private set; } = DateTime.UtcNow;
        public Money Price { get; private set; }

        public PriceLabel(ProductItemId productItemId, Money price)
        {
            ProductItemId = productItemId;
            Price = price;
        }
    }
}
