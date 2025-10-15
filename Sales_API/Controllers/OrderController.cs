using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Api.Contracts.OrderContracts.Commands;
using Sales.Api.Contracts.OrderContracts.Queries;
using Sales.Domain.OrderAgg.Models;
using Sales.Infrastructure.Persistence;

namespace Sales.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly SalesDbContext salesContext;
    private readonly IMediator mediator;

    public OrderController(SalesDbContext salesContext)
    {
        this.salesContext = salesContext;
    }

    //Post SubmitOrder
    [HttpPost("submit")]
    public IActionResult SubmitOrder(SubmitOrderCommand cmd)
    {
        CreateBillRequest vo = new CreateBillRequest()
        {
            Custom = cmd.UserId
        };
        var userCurrentCart = salesContext.ShoppingCarts.SingleOrDefault(x => x.customerId == cmd.UserId);
        if (userCurrentCart == null)
            throw new Exception("Cart not found");
        var order = new Order(cmd.UserId, userCurrentCart.Id, vo);
        salesContext.Orders.Add(order);
        salesContext.SaveChanges();
        return Ok();
    }
    //Get GetOrder
    [HttpGet("{id}")]
    public IActionResult GetOrder(long id)
    {
        var order = salesContext.Orders
            .FirstOrDefault(x => x.Id == id);
        if (order == null)
            throw new Exception("Order not found");

        GetOrderQueryResponse resp = new()
        {
            Id = id,
            ReceiverInformation = order.ReceiverInfo,
            ShoppingCartItems = order.ShoppingCart.ShoppingCartItems
        };



        return Ok(resp);
    }
    //Get[] GetAllUserOrders

    //Get[] GetAllOrders

}
