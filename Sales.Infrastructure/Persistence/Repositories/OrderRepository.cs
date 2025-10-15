using Microsoft.EntityFrameworkCore;
using Sales.Domain.OrderAgg.Contracts;
using Sales.Domain.OrderAgg.Models;
using System.Linq.Expressions;

namespace Sales.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesDbContext salesContext;

        public OrderRepository(SalesDbContext salesContext)
        {
            this.salesContext = salesContext;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await salesContext.Orders.AddAsync(order);
            return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await salesContext.Orders.ToListAsync();
        }

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            return await salesContext.Orders.FirstOrDefaultAsync(predicate);
        }



        public async Task<Order> GetOrThrowExceptionAsync(Expression<Func<Order, bool>> predicate)
        {
            var obj = await GetAsync(predicate);
            if (obj == null)
                throw new KeyNotFoundException();
            return obj;
        }


        public Task<Order> UpdateAsync(Order order)
        {

            if (order == null || order.Id.Value == default)
                throw new ArgumentException("Order must have a valid primary key.", nameof(order));

            var trackedEntity = salesContext.ChangeTracker.Entries<Order>()
                                        .FirstOrDefault(e => e.Entity.Id == order.Id);

            if (trackedEntity != null)
            {
                trackedEntity.CurrentValues.SetValues(order);
            }
            else
            {
                salesContext.Attach(order);
                salesContext.Entry(order).State = EntityState.Modified;
            }
            return Task.FromResult(order);

        }
    }
}
