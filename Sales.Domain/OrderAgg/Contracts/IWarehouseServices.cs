using Sales.Domain.Common.ValueObjects;

namespace Sales.Domain.OrderAgg.Contracts
{
    public interface IWarehouseServices
    {
        Task<bool> ReserveItems(List<ProductItemQuantity> productItemQuantities);
    }
}
