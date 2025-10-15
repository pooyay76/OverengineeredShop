using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.Api.Contracts.ShoppingCartContracts.Commands;
using Sales.Domain.ShoppingCartAgg.Models;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShoppingCartController(IMediator mediator)
        {
            this.mediator = mediator;
        }



        [HttpPut("addItem")]
        public IActionResult AddItem(AddProductItemCommand command)
        {
            ShoppingCart cart = salesContext.ShoppingCarts.Include(x => x.ShoppingCartItems).FirstOrDefault(x => x.UserId == command.UserId &&
            x.IsPaid == false);
            if (cart == null)
            {
                cart = new ShoppingCart(command.UserId, false);
                cart.AddItem(command.ProductItemId, command.Quantity);
                salesContext.ShoppingCarts.Add(cart);
            }

            salesContext.SaveChanges();
            return Ok();
        }
        [HttpGet("get/{userId}")]
        public IActionResult GetCurrentCart(Guid userId)
        {
            ShoppingCart cart = salesContext.ShoppingCarts.Include(x => x.ShoppingCartItems).FirstOrDefault(x => x.UserId == userId &&
            x.IsPaid == false);
            if (cart == null)
            {
                cart = new ShoppingCart(userId, false);
                salesContext.ShoppingCarts.Add(cart);
            }
            salesContext.SaveChanges();

            return Ok(cart);
        }
        //Get DecrementItemQuantity(long userId,long itemId)
        //Get RemoveItem(long userId,long itemId)

    }
}
