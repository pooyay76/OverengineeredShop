using Sales.Domain.Common.ValueObjects;
using Sales.Domain.OrderAgg.Models;

namespace Sales.Application.OrderUseCases.Queries
{
    public class GetOrderQueryResponse
    {
        public OrderId Id { get; set; }
        public List<ProductItemQuantity> ShoppingCartItems { get; set; }
        public ShippingInformation ReceiverInformation { get; set; }
    }
}
