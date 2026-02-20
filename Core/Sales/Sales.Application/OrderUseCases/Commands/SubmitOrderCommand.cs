using Common.Application.Base;
using Common.Application.DTOs;
using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;
using Sales.Domain.OrderAgg;

namespace Sales.Application.OrderUseCases.Commands
{
    public class SubmitOrderCommand : CommandBase
    {
        public UserId CustomerId { get; set; }
        public BillId BillId { get; set; }

    }
    public class SubmitOrderCommandHandler : CommandHandlerBase<SubmitOrderCommand, ApplicationResponse>
    {
        private readonly OrderManager orderManager;

        public SubmitOrderCommandHandler(OrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        public override async Task<ApplicationResponse> HandleAsync(SubmitOrderCommand request)
        {
            var order = await orderManager.CreateOrderAsync(request.BillId, request.CustomerId);
            var response = new ApplicationResponse();
            return response.Succeeded();
        }
    }
}
