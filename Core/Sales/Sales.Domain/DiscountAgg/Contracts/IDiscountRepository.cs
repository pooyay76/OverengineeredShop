using Sales.Domain.DiscountAgg.Models;

namespace Sales.Domain.DiscountAgg.Contracts
{
    public interface IDiscountRepository
    {
        Task AddAsync(Discount discount);
        Task<Discount> UpdateAsync(Discount discount);


        Task<Discount> GetAsync(DiscountId id);
        Task<List<Discount>> GetAllAsync();
        Task<Discount> GetOrThrowExceptionAsync(DiscountId id);
        Task<List<Discount>> GetActiveDiscountsAsync();
    }
}
