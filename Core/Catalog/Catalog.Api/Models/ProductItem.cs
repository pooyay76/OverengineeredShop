using Catalog.Api.Exceptions;
using Catalog.Api.Models.ValueObjects;

namespace Catalog.Api.Models
{
    public class ProductItem
    {
        public long Id { get;private set; }
        public long ProductId { get; private set; }
        public Product Product { get; private set; }
        public Money Price { get; private set; }
        public Dictionary<string, string> ItemFeatures { get; private set; } = [];

        public ProductItem(long productId, Money price, Dictionary<string, string> itemFeatures=null)
        {
            ProductId = productId;
            Price = price;
            ItemFeatures = itemFeatures ?? [];
        }
        private ProductItem()
        {
            
        }
        public void EditFeatures(Dictionary<string, string> itemFeatures)
        {
            ItemFeatures = itemFeatures ?? [];
        }

        public void SetPrice(Money price)
        {
            if (price == null || price.Amount < 0)
            {
                throw new InvalidProductPriceException();
            }
            Price = price;
        }
    }
}
