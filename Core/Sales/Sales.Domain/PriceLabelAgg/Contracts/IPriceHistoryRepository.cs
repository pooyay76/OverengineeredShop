using Sales.Domain.PriceLabelAgg.Models;
using System.Linq.Expressions;

namespace Sales.Domain.PriceLabelAgg.Contracts
{
    public interface IPriceHistoryRepository
    {
        Task<List<PriceLabel>> GetLatestPricesAsync(Expression<Func<PriceLabel, bool>> predicate);
        Task<PriceLabel> Add(PriceLabel entity);
    }
}
