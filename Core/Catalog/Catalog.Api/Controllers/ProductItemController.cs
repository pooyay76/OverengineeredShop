using Catalog.Api.Commands;
using Catalog.Api.Data;
using Catalog.Api.Models;
using Common.Domain.Language.Global.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly CatalogContext catalogContext;

        public ProductItemController(CatalogContext catalogContext,
             IConfiguration configuration)
        {
            this.catalogContext = catalogContext;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProductItemsAsync()
        {
            var productItems = await catalogContext.ProductItems.ToListAsync();
            return Ok(productItems);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateProductItemAsync([FromBody]CreateProductItemCommand command)
        {


            var priceMoney = new Money(command.Price);
            var productItem = new ProductItem(command.ProductId, priceMoney, command.ItemFeatures);


            var productExists = await catalogContext.Products.AnyAsync(x=>x.Id == command.ProductId);
            if(productExists == false)
            {
                throw new KeyNotFoundException("Product does not exist");
            }


            await catalogContext.ProductItems.AddAsync(productItem);
            await catalogContext.SaveChangesAsync();

            return Ok(new { ProductItemId = productItem.Id });


        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditProductItemAsync(EditProductItemCommand command)
        {
            var priceMoney = new Money(command.Price);
            var productItem = await catalogContext.ProductItems.FindAsync(command.ProductItemId);
            if(productItem == null)
                return NotFound();

            productItem.SetPrice(priceMoney);

            if(command.ItemFeatures != null && command.ItemFeatures.Count > 0 ) 
                productItem.SetItemFeatures(command.ItemFeatures);

            await catalogContext.SaveChangesAsync();

            return Ok(new { ProductItemId = productItem.Id });
        }
    }
}
