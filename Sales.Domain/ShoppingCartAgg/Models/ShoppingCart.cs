using Sales.Domain._common;
using Sales.Domain._common.Base;

namespace Sales.Domain.ShoppingCartAgg.Models
{
    public class ShoppingCart : AggregateRoot<CustomerId>
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = [];

        public ShoppingCart(CustomerId customerId)
        {
            //Shopping cart's id is equal to customer id
            Id = customerId;
        }
        private ShoppingCart()
        {

        }

        public void AddItem(ProductItemId productItemId, int quantity)
        {
            var item = ShoppingCartItems.FirstOrDefault(x => x.ProductItemId == productItemId);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                ShoppingCartItems.Add(new ShoppingCartItem()
                {
                    ProductItemId = productItemId,
                    Quantity = quantity
                });
            }

        }
        public void DecrementItemQuantity(ProductItemId productItemId, int quantity)
        {
            var item = ShoppingCartItems.FirstOrDefault(x => x.ProductItemId == productItemId);

            if (item == null)
                return;

            if (item.Quantity <= quantity)
            {
                ShoppingCartItems.Remove(item);
            }
            else
            {
                item.Quantity -= quantity;
            }

        }
        public void ClearItems(List<ProductItemId> productItemIds)
        {
            ShoppingCartItems.RemoveAll(x => productItemIds.Contains(x.ProductItemId));
        }
    }
}
