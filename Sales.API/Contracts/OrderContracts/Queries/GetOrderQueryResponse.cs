
using Sales.Domain.Common.ValueObjects;

namespace Sales.Api.Contracts.OrderContracts.Queries
{
    public class GetOrderQueryResponse
    {
        public long Id { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShippingInformation ReceiverInformation { get; set; }
    }
}
