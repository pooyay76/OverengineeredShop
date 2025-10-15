
using Sales.Api.Contracts.Dtos;

namespace Sales.Api.Contracts.ShoppingCartContracts.Queries
{
    public class GetShoppingCartQueryResponse
    {
        public List<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
