using Sales.Domain.BillAgg.Models;
using System.Linq.Expressions;

namespace Sales.Domain.BillAgg.Contracts
{
    public interface IBillRepository
    {
        Task CreateAsync(Bill bill);
        Task<Bill> GetAsync(Expression<Func<Bill, bool>> predicate);
        Task<Bill> GetOrThrowAsync(Expression<Func<Bill, bool>> predicate);
        Task<Bill> GetActiveAsync(Expression<Func<Bill, bool>> predicate);
    }
}
