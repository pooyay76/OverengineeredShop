using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.BillUseCases.Commands;
using Sales.Application.DTOs;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.Common.ValueObjects;
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

        public record ProceedToPaymentRequest
        {
            public Guid CustomerId;
            public string ReceiverFirstName { get; init; }
            public string ReceiverLastName { get; init; }


            public ShippingType ShippingType { get; init; }
            public string Country { get; init; }
            public string Province { get; init; }
            public string City { get; init; }
            public string Address { get; init; }
            public string PostalCode { get; init; }
            public string ContactPhoneNumber { get; init; }

        }

        [HttpPost("proceed")]
        public async Task<ActionResult<string>> ProceedToPaymentPage([FromBody] ProceedToPaymentRequest req)
        {
            var command = new CreateBillCommand
            {
                ShippingInformation = new ShippingInformation
                {
                    Address = req.Address,
                    City = req.City,
                    ContactPhoneNumber = req.ContactPhoneNumber,
                    Country = req.Country,
                    PostalCode = req.PostalCode,
                    Province = req.Province,
                    ReceiverFirstName = req.ReceiverFirstName,
                    ReceiverLastName = req.ReceiverLastName,
                    ShippingType = req.ShippingType
                },
                CustomerType = CustomerType.Normal,
                CustomerId = new CustomerId(req.CustomerId)
            };

            CommandResponseGeneric<Bill> resp = await mediator.Send(command);
            return paymentGatewayService.GetPaymentPageUrl(resp.Result.PaymentSessionId);


        }
    }
}
