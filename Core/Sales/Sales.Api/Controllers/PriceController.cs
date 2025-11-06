using Microsoft.AspNetCore.Mvc;
using Sales.Api.Contracts.ProductItemPriceContracts.Commands;
using Sales.Api.Contracts.ProductItemPriceContracts.Queries;
using Sales.Domain.Common;
using Sales.Domain.PriceLabelAgg.Models;
using Sales.Infrastructure.Persistence;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly SalesDbContext salesContext;

        public PriceController(SalesDbContext salesContext)
        {
            this.salesContext = salesContext;
        }

        [HttpGet("latest-prices")]
        public IActionResult GetLatestPrice([FromQuery] List<ProductItemId> productItemIds)
        {
            var latestPrices = salesContext
                .ProductItemPrices.Where(x => productItemIds.Contains(x.ProductItemId))
                .GroupBy(x => x.ProductItemId).Select(x =>
                x.OrderByDescending(y => y.CreationDateTime).FirstOrDefault().Id);

            var resp = salesContext.ProductItemPrices
                .Where(x => latestPrices.Any(y => x.Id == y)).
                Select(x => new GetLatestProductItemPriceQueryResponse
                {
                    Price = x.Price,
                    ProductItemId = x.ProductItemId
                }).ToList();

            if (resp == null || resp.Count() == 0)
                throw new ArgumentException("No price for given products");

            return Ok(resp);
        }


        [HttpPost("add")]
        public IActionResult AddProductItemPrice(AddProductItemPriceCommand command)
        {
            PriceLabel newPrice = new(command.ProductItemId, command.Price);
            salesContext.ProductItemPrices.Add(newPrice);
            salesContext.SaveChanges();
            return Ok();
        }
    }
}
