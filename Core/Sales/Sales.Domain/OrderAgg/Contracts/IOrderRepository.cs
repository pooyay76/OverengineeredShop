using Sales.Domain.OrderAgg.Models;
using System.Linq.Expressions;

namespace Sales.Domain.OrderAgg.Contracts
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<Order> GetAsync(Expression<Func<Order, bool>> predicate);
        Task<Order> GetOrThrowExceptionAsync(Expression<Func<Order, bool>> predicate);
        Task<List<Order>> GetAllAsync();
    }
}
