using Catalog.Api.Exceptions;
using Common.Domain.Base;
using Common.Domain.Language.Catalog.Events;
using Common.Domain.Language.Catalog.Events.Global;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;

namespace Catalog.Api.Models
{
    public class ProductItem : AggregateRootBase<ProductItemId>
    {
        public ProductId ProductId { get; private set; }
        public Product Product { get; private set; }
        public Money Price { get; private set; } = new(0);
        public Dictionary<string, string> ItemFeatures { get; private set; } = [];

        public ProductItem(ProductId productId, Money price, Dictionary<string, string> itemFeatures = null)
        {
            AddEvent(new ProductItemCreatedEvent(Id, productId, price, itemFeatures));
        }
        public void Apply(ProductItemCreatedEvent @event)
        {
            Id = @event.Id;
            ProductId = @event.ProductId;
            Price = @event.Price;
            ItemFeatures = @event.ItemFeatures ?? [];
        }



        private ProductItem()
        {

        }



        public void SetItemFeatures(Dictionary<string, string> itemFeatures)
        {
            if (ItemFeatures.SequenceEqual(itemFeatures) == false && itemFeatures != null)
            {
                AddEvent(new ProductItemFeaturesChanged()
                {
                    ItemFeatures = ItemFeatures,
                    Id = Id
                });
            }

        }
        public void Apply(ProductItemFeaturesChanged @event)
        {
            ItemFeatures = @event.ItemFeatures ?? [];
        }




        public void SetPrice(Money price)
        {

            if (price != null && Price != price)
            {
                ValidatePrice(price);
                AddEvent(new NewPriceAddedEvent(Id, price));
            }

        }
        public void Apply(NewPriceAddedEvent @event)
        {
            Price = @event.Price;
        }

        private void ValidatePrice(Money price)
        {
            //zero is an acceptable price, meaning the product is not for sale yet
            if (price.GetValue() < 0)
            {
                AddError(InvalidProductPriceException.DefaultMessage);
            }
        }
    }
}
