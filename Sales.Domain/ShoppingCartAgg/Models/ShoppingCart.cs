using Sales.Domain.Common;
using Sales.Domain.Common.Base;

namespace Sales.Domain.ShoppingCartAgg.Models
{
    public class ShoppingCart : AggregateRoot<CustomerId>
    {
        internal List<ShoppingCartItem> _shoppingCartItems { get; set; } = [];
        public IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems.AsReadOnly();
        internal ShoppingCart(CustomerId customerId)
        {
            //Shopping cart's id is equal to customer id
            Id = customerId;
        }
        private ShoppingCart()
        {

        }

        internal void AddItem(ShoppingCartItem item)
        {
            var currentItem = _shoppingCartItems.FirstOrDefault(x => x.ProductItemId == item.ProductItemId);

            if (currentItem != null)
            {
                currentItem.Quantity += item.Quantity;
            }
            else
            {
                _shoppingCartItems.Add(item);
            }

        }
        internal void DecrementItemQuantity(ProductItemId productItemId, int quantity)
        {
            var item = _shoppingCartItems.FirstOrDefault(x => x.ProductItemId == productItemId);

            if (item == null)
                return;

            if (item.Quantity <= quantity)
            {
                _shoppingCartItems.Remove(item);
            }
            else
            {
                item.Quantity -= quantity;
            }

        }
        internal void ClearItems(List<ProductItemId> productItemIds)
        {
            _shoppingCartItems.RemoveAll(x => productItemIds.Contains(x.ProductItemId));
        }
    }
}
