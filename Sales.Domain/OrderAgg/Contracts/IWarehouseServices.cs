using Sales.Domain._common.ValueObjects;

namespace Sales.Domain.OrderAgg.Contracts
{
    public interface IWarehouseServices
    {
        Task<bool> ReserveItems(List<ProductItemQuantity> productItemQuantities);
    }
}
