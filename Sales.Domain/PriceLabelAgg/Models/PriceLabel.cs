using Sales.Domain.Common;
using Sales.Domain.Common.ValueObjects;

namespace Sales.Domain.PriceLabelAgg.Models
{
    public class PriceLabel
    {
        public long Id { get; private set; }
        public ProductItemId ProductItemId { get; private set; }
        public DateTime CreationDateTime { get; private set; } = DateTime.UtcNow;
        public Money Price { get; private set; }

        private PriceLabel()
        {

        }
        public PriceLabel(ProductItemId productItemId, Money price)
        {
            ProductItemId = productItemId;
            Price = price;
        }
    }
}
