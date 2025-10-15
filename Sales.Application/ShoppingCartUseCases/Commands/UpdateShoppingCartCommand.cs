using MediatR;
using Sales.Application.Contracts;
using Sales.Application.DTOs;
using Sales.Domain._common;
using Sales.Domain._common.ValueObjects;
using Sales.Domain.ShoppingCartAgg.Contracts;
using Sales.Domain.ShoppingCartAgg.Models;

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
        public UpdateShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            CommandResponse resp = new();
            ShoppingCart cart = new(request.CustomerId);
            await shoppingCartRepository.UpdateAsync(cart);
            await unitOfWork.CommitTransactions();
            return resp.Succeeded();

        }
    }

}
