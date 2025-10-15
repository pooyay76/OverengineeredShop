using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.DTOs;
using Sales.Domain._common.ValueObjects;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.BillAgg.Models;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IPaymentGatewayService paymentGatewayService;
        public BillController(IMediator mediator, IPaymentGatewayService paymentGatewayService)
        {
            this.mediator = mediator;
            this.paymentGatewayService = paymentGatewayService;
        }

        public async Task<ActionResult<string>> ProceedToPaymentPage(Guid customerId, CustomerType customerType
            , ShippingInformation shippingInfo)
        {
            var command = new CreateBillCommand
            {
                ShippingInformation = shippingInfo,
                CustomerType = customerType,
                CustomerId = customerId
            };

            CommandResponseGeneric<Bill> resp = await mediator.Send(command);
            return paymentGatewayService.GetPaymentPageUrl(resp.Result.PaymentSessionId);


        }
    }
}
