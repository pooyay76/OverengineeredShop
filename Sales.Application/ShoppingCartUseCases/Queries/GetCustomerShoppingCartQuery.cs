
using MediatR;
using Sales.Domain._common;
using Sales.Domain._common.ValueObjects;
using Sales.Domain.ShoppingCartAgg.Contracts;

namespace Sales.Application.ShoppingCartUseCases.Queries
{
    public class GetCustomerShoppingCartQuery : IRequest<GetUserShoppingCartQueryDto>
    {
        public CustomerId CustomerId { get; init; }
    }
    public class GetUserShoppingCartQueryHandler : IRequestHandler<GetCustomerShoppingCartQuery, GetUserShoppingCartQueryDto>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        public GetUserShoppingCartQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<GetUserShoppingCartQueryDto> Handle(GetCustomerShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var obj = await shoppingCartRepository.GetOrThrowAsync(x => x.Id == request.CustomerId);
            GetUserShoppingCartQueryDto result = new()
            {
                Items = obj.ShoppingCartItems.Select(x => new GetUserShoppingCartQueryDto()
                {
                    Items = x.S
                }
                )
            };
        }
    }
    public class GetUserShoppingCartQueryDto
    {
        public List<ProductItemQuantity> Items { get; set; }
    }

}
