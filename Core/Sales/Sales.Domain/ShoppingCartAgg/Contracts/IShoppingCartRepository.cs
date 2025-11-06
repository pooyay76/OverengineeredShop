using Sales.Domain.ShoppingCartAgg.Models;
using System.Linq.Expressions;

namespace Sales.Domain.ShoppingCartAgg.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> UpdateAsync(ShoppingCart ShoppingCart);
        Task<List<ShoppingCart>> GetAllAsync();
        Task<ShoppingCart> GetOrThrowAsync(Expression<Func<ShoppingCart, bool>> predicate);
        Task<ShoppingCart> GetAsync(Expression<Func<ShoppingCart, bool>> predicate);
    }
}
