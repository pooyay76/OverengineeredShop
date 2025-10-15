using Sales.Domain.PriceHistoryAgg.Models;
using System.Linq.Expressions;

namespace Sales.Domain.PriceHistoryAgg.Contracts
{
    public interface IPriceHistoryRepository
    {
        Task<List<PriceLabel>> GetLatestPricesAsync(Expression<Func<PriceLabel, bool>> predicate);
        Task<PriceLabel> Add(PriceLabel entity);
    }
}
