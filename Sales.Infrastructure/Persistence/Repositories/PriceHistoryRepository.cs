using Microsoft.EntityFrameworkCore;
using Sales.Domain.PriceLabelAgg.Contracts;
using Sales.Domain.PriceLabelAgg.Models;
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

        public Task<PriceLabel> Add(PriceLabel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PriceLabel>> GetLatestPricesAsync(Expression<Func<PriceLabel, bool>> predicate)
        {

            return await salesDbContext.ProductItemPrices.Where(predicate).ToListAsync();

        }
    }
}
