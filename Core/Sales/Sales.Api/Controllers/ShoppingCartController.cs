using Common.Application;
using Common.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.ShoppingCartUseCases.Commands;
using Sales.Application.ShoppingCartUseCases.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("sales/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly Mediator mediator;

        public ShoppingCartController(Mediator mediator)
        {
            this.mediator = mediator;
        }



        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateShoppingCartCommand command)
        {
            return Ok(await mediator.SendAsync<ApplicationResponse>(command));
        }

        //Get DecrementItemQuantity(long userId,long itemId)
        //Get RemoveItem(long userId,long itemId)

    }
}
