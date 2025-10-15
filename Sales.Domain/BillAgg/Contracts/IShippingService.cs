using Sales.Domain._common.ValueObjects;

namespace Sales.Domain.BillAgg.Contracts
{
    public interface IShippingService
    {

        Money GetShippingCost(Money ShipmentValue, List<ProductItemQuantity> orderItems, ShippingInformation destination);
    }
}