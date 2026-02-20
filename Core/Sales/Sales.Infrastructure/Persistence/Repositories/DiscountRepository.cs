using Common.Domain.Language.Sales.ValueObjects;
using Sales.Domain.DiscountAgg.Contracts;
using Sales.Domain.DiscountAgg.Models;

namespace Sales.Infrastructure.Persistence.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly SalesDbContext salesDbContext;

        public DiscountRepository(SalesDbContext salesDbContext)
        {
            this.salesDbContext = salesDbContext;
        }

        public async Task AddAsync(Discount discount)
        {
            await salesDbContext.AddAsync(discount);
        }

        public Task<List<Discount>> GetActiveDiscountsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Discount>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Discount> GetAsync(DiscountId id)
        {
            throw new NotImplementedException();
        }

        public Task<Discount> GetOrThrowExceptionAsync(DiscountId id)
        {
            throw new NotImplementedException();
        }

        public Task<Discount> UpdateAsync(Discount discount)
        {
            throw new NotImplementedException();
        }
    }
}
