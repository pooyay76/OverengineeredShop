using Common.Application;
using Common.Application.DTOs;
using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Sales.Api.Contracts.OrderContracts.Queries;
using Sales.Application.OrderUseCases.Commands;
using Sales.Application.OrderUseCases.Queries;
using Sales.Infrastructure.Persistence;

namespace Sales.Api.Controllers;

[ApiController]
[Route("sales/[controller]")]
public class OrderController : ControllerBase
{
    private readonly SalesDbContext salesContext;
    private readonly Mediator mediator;

    public OrderController(SalesDbContext salesContext, Mediator mediator)
    {
        this.salesContext = salesContext;
        this.mediator = mediator;
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitOrderAsync(SubmitOrderCommand cmd)
    {

        var response = await mediator.SendAsync<ApplicationResponse>(cmd);
        return Ok(response);
    }
    ////Get GetOrder
    //[HttpGet("{id}")]
    //public IActionResult GetOrder(OrderId id)
    //{
    //    var order = salesContext.Orders
    //        .FirstOrDefault(x => x.Id == id);
    //    var bill = salesContext.Bills.FirstOrDefault(x => x.Id == order.BillId);
    //    if (order == null)
    //        throw new Exception("Order not found");
    //    if (bill == null)
    //        throw new Exception("Database error, please contact administrator");

    //    GetOrderQueryResponse resp = new()
    //    {
    //        Id = id,
    //        ReceiverInformation = bill.ShippingInformation,
    //        ShoppingCartItems = bill.BillItems.Select(x => new ProductItemQuantity()
    //        {
    //            ProductItemId = x.ProductItemId,
    //            Quantity = x.Quantity
    //        }).ToList()
    //    };



    //    return Ok(resp);
    //}


}
