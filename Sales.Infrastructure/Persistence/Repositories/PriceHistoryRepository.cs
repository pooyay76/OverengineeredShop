using Microsoft.EntityFrameworkCore;
using Sales.Domain.PriceHistoryAgg.Contracts;
using Sales.Domain.PriceHistoryAgg.Models;
using System.Linq.Expressions;

namespace Sales.Infrastructure.Persistence.Repositories
{
    public class PriceHistoryRepository : IPriceHistoryRepository
    {
        private readonly SalesDbContext salesDbContext;

        public PriceHistoryRepository(SalesDbContext salesDbContext)
        {
            this.salesDbContext = salesDbContext;
        }


        public async Task<List<PriceLabel>> GetLatestPricesAsync(Expression<Func<PriceLabel, bool>> predicate)
        {

            return await salesDbContext.ProductItemPrices.Where(predicate).ToListAsync();

        }
    }
}
