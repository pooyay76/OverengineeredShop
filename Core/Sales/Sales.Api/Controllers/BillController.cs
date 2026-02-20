using Common.Application;
using Common.Application.DTOs;
using Common.Domain.Language.Enums;
using Common.Domain.Language.Global.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.BillUseCases.Commands;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.BillAgg.Models;
using System.Security.Cryptography;
namespace Sales.Api.Controllers
{
    [Route("sales/[controller]")]
    [ApiController]
    public partial class BillController : ControllerBase
    {
        private readonly Mediator mediator;
        private readonly IPaymentGatewayService paymentGatewayService;
        public BillController(Mediator mediator, IPaymentGatewayService paymentGatewayService)
        {
            this.mediator = mediator;
            this.paymentGatewayService = paymentGatewayService;
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
                CustomerType = UserType.Normal,
                CustomerId = new UserId(req.CustomerId)
            };
            ApplicationResponse<Bill> resp = await mediator.SendAsync<ApplicationResponse<Bill>>(command);
            return paymentGatewayService.GetPaymentPageUrl(resp.Result.PaymentSessionId);


        }
    }
}
