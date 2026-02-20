using Common.Application.Base;
using Common.Application.Contracts;
using Common.Application.DTOs;
using Common.Domain.Language.Global.ValueObjects;
using Sales.Domain.ShoppingCartAgg;
using Sales.Domain.ShoppingCartAgg.Contracts;

namespace Sales.Application.ShoppingCartUseCases.Commands
{
    public class UpdateShoppingCartCommand : CommandBase
    {
        public UserId CustomerId { get; set; }
        public List<ProductItemQuantity> CartItems { get; set; }

    }
    public class UpdateShoppingCartCommandHandler : CommandHandlerBase<UpdateShoppingCartCommand, ApplicationResponse>
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IFrameworkUnitOfWork unitOfWork;
        private readonly ShoppingCartManager shoppingCartManager;
        public UpdateShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository, IFrameworkUnitOfWork unitOfWork, ShoppingCartManager shoppingCartManager)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.unitOfWork = unitOfWork;
            this.shoppingCartManager = shoppingCartManager;
        }


        public override async Task<ApplicationResponse> HandleAsync(UpdateShoppingCartCommand request)
        {
            ApplicationResponse resp = new();
            var cart = await shoppingCartManager.CreateOrGetCustomerCartAsync(request.CustomerId);
            foreach (var item in request.CartItems)
            {
                await shoppingCartManager.UpdateCartItemAsync(cart, item);
            }
            await shoppingCartRepository.UpdateAsync(cart);
            await unitOfWork.CommitAsync();
            return resp.Succeeded();
        }
    }

}
