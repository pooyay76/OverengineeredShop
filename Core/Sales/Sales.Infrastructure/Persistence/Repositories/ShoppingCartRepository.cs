using Microsoft.EntityFrameworkCore;
using Sales.Domain.ShoppingCartAgg.Contracts;
using Sales.Domain.ShoppingCartAgg.Models;
using System.Linq.Expressions;

namespace Sales.Infrastructure.Persistence.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly SalesDbContext salesDbContext;

        public ShoppingCartRepository(SalesDbContext salesDbContext)
        {
            this.salesDbContext = salesDbContext;
        }


        public Task<List<ShoppingCart>> GetAllAsync()
        {
            return salesDbContext.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCart> GetAsync(Expression<Func<ShoppingCart, bool>> predicate)
        {
            return await salesDbContext.ShoppingCarts.FirstOrDefaultAsync(predicate);
        }

        public async Task<ShoppingCart> GetOrThrowAsync(Expression<Func<ShoppingCart, bool>> predicate)
        {
            var obj = await GetAsync(predicate);
            if (obj == null)
                throw new KeyNotFoundException();
            else
                return obj;
        }

        public Task<ShoppingCart> UpdateAsync(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
                throw new ArgumentNullException(nameof(shoppingCart));

            salesDbContext.ShoppingCarts.Update(shoppingCart);
            return Task.FromResult(shoppingCart);
        }
    }
}
