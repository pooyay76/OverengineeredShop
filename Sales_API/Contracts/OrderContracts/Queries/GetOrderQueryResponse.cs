using Sales_API.Models.OrderAgg;

namespace Sales.Api.Contracts.OrderContracts.Queries
{
    public class GetOrderQueryResponse
    {
        public long Id { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ReceiverInformationValueObject ReceiverInformation { get; set; }
    }
}
