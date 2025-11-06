using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.ShoppingCartUseCases.Commands;
using Sales.Application.ShoppingCartUseCases.Queries;

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



        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateShoppingCartCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetCurrentCartAsync(GetCustomerShoppingCartQuery request)
        {
            return Ok(await mediator.Send(request));
        }
        //Get DecrementItemQuantity(long userId,long itemId)
        //Get RemoveItem(long userId,long itemId)

    }
}
