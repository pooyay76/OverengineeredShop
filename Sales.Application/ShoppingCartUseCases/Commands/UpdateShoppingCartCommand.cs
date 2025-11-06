using MediatR;
using Sales.Application.Contracts;
using Sales.Application.DTOs;
using Sales.Domain.Common;
using Sales.Domain.Common.ValueObjects;
using Sales.Domain.ShoppingCartAgg;
using Sales.Domain.ShoppingCartAgg.Contracts;

namespace Sales.Application.ShoppingCartUseCases.Commands
{
    public class UpdateShoppingCartCommand : IRequest<CommandResponse>
    {
        public CustomerId CustomerId { get; set; }
        public List<ProductItemQuantity> CartItems { get; set; }

    }
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, CommandResponse>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ShoppingCartManager shoppingCartManager;
        public UpdateShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork, ShoppingCartManager shoppingCartManager)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.unitOfWork = unitOfWork;
            this.shoppingCartManager = shoppingCartManager;
        }

        public async Task<CommandResponse> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            CommandResponse resp = new();
            var cart = await shoppingCartManager.CreateOrGetCustomerCartAsync(request.CustomerId);
            foreach(var item in request.CartItems)
            {
                await shoppingCartManager.UpdateCartItemAsync(cart, item);
            }
            await shoppingCartRepository.UpdateAsync(cart);
            await unitOfWork.CommitTransactions();
            return resp.Succeeded();

        }
    }

}
