using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;

namespace Sales.Api.Contracts.OrderContracts.Queries
{
    public class GetOrderQueryResponse
    {
        public OrderId Id { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShippingInformation ReceiverInformation { get; set; }
    }
}
