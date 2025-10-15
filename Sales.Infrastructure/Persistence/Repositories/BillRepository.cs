using Microsoft.EntityFrameworkCore;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.BillAgg.Models;
using System.Linq.Expressions;

namespace Sales.Infrastructure.Persistence.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly SalesDbContext salesDbContext;

        public BillRepository(SalesDbContext salesDbContext)
        {
            this.salesDbContext = salesDbContext;
        }

        public async Task CreateAsync(Bill bill)
        {
            await salesDbContext.AddAsync(bill);
        }

        public async Task<Bill> GetActiveAsync(Expression<Func<Bill, bool>> predicate)
        {
            return await salesDbContext.Bills.Where(x => x.ExpirationDateTime > DateTime.UtcNow)
                .FirstOrDefaultAsync(predicate);
        }

        public Task<Bill> GetAsync(Expression<Func<Bill, bool>> predicate)
        {
            return salesDbContext.Bills.FirstOrDefaultAsync(predicate);
        }

        public async Task<Bill> GetOrThrowAsync(Expression<Func<Bill, bool>> predicate)
        {
            var obj = await GetAsync(predicate);
            if (obj == null)
                throw new KeyNotFoundException();
            return obj;
        }
    }
}
