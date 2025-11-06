using MediatR;
using Sales.Application.DTOs;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.OrderAgg;

namespace Sales.Application.OrderUseCases.Commands
{
    public class SubmitOrderCommand : IRequest<CommandResponse>
    {
        public CustomerId CustomerId { get; set; }
        public BillId BillId { get; set; }

    }
    public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, CommandResponse>
    {
        private readonly OrderManager orderManager;

        public SubmitOrderCommandHandler(OrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        public async Task<CommandResponse> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderManager.CreateOrderAsync(request.BillId, request.CustomerId);
            var response = new CommandResponse();
            return response.Succeeded();
        }
    }
}
