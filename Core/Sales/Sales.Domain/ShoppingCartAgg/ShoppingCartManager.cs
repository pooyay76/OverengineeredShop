using Sales.Domain.Common;
using Sales.Domain.Common.Base;
using Sales.Domain.Common.ValueObjects;
using Sales.Domain.External;
using Sales.Domain.ShoppingCartAgg.Contracts;
using Sales.Domain.ShoppingCartAgg.Models;

namespace Sales.Domain.ShoppingCartAgg
{
    public class ShoppingCartManager
    {
        private readonly ICatalogClient catalogServices;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartManager(ICatalogClient productExistsService, IShoppingCartRepository shoppingCartRepository)
        {
            this.catalogServices = productExistsService;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> AddItem(ShoppingCart cart, ShoppingCartItem item)
        {
            var existingCartItem = cart._shoppingCartItems.FirstOrDefault(x => x.ProductItemId == item.ProductItemId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += item.Quantity;
            }

            var isValid = await catalogServices.IsProductItemValidAsync(item.ProductItemId);
            if (isValid)
                cart.AddItem(item);
            return cart;
        }
        public async Task<ShoppingCart> UpdateCartItemAsync(ShoppingCart cart, ProductItemQuantity productItem)
        {
            var cartItem = new ShoppingCartItem()
            {
                ProductItemId = productItem.ProductItemId,
                Quantity = productItem.Quantity
            };
            if (cartItem.Quantity <= 0)
                throw new DomainException("Item qunatity can't be less than 1");


            var existingCartItem = cart._shoppingCartItems.FirstOrDefault(x => x.ProductItemId == cartItem.ProductItemId);
            if (existingCartItem == null)
                return await AddItem(cart, cartItem);
            else
            {
                existingCartItem.Quantity = cartItem.Quantity;
            }
            return cart;
        }
        public Task<ShoppingCart> RemoveCartItem(ShoppingCart cart, ShoppingCartItem item)
        {
            var cartItem = cart._shoppingCartItems.FirstOrDefault(x => x.ProductItemId == item.ProductItemId);
            if (cartItem == null)
                return Task.FromResult(cart);
            else
            {
                cart._shoppingCartItems.Remove(cartItem);
            }
            return Task.FromResult(cart);
        }
        public async Task<ShoppingCart> CreateOrGetCustomerCartAsync(CustomerId customerId)
        {

            ShoppingCart cart = await shoppingCartRepository.GetAsync(x => x.Id == customerId);

            if (cart == null)
            {
                cart = new(customerId);

            }


            return cart;

        }
    }
}
