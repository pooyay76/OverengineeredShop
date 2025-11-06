using Catalog.Api.Commands;
using Catalog.Api.Contracts.Interfaces;
using Catalog.Api.Data;
using Catalog.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly CatalogContext catalogContext;
        private readonly IKafkaProducerService kafkaProducerService;

        public ProductItemController(CatalogContext catalogContext, IKafkaProducerService kafkaProducerService)
        {
            this.catalogContext = catalogContext;
            this.kafkaProducerService = kafkaProducerService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProductItemsAsync()
        {
            var productItems = await catalogContext.ProductItems.ToListAsync();
            return Ok(productItems);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateProductItemAsync([FromForm]CreateProductItemCommand command)
        {

            var productItem = new ProductItem(command.ProductId, command.Price, command.ItemFeatures);
            await catalogContext.ProductItems.AddAsync(productItem);
            await catalogContext.SaveChangesAsync();
            if (command.Price.Amount != 0)
            {
                await kafkaProducerService.ProduceAsync(productItem.Id.ToString(), command.Price.Amount.ToString());
            }
            return Ok(new { ProductItemId = productItem.Id });
        }
        [HttpPut("edit")]
        public async Task<IActionResult> EditProductItemAsync(EditProductItemCommand command)
        {

            var productItem = await catalogContext.ProductItems.FindAsync(command.ProductItemId);
            if(productItem == null)
                return NotFound();
            if (productItem.Price != command.Price)
            {
                await kafkaProducerService.ProduceAsync(command.ProductItemId.ToString(), command.Price.ToString());
                productItem.SetPrice(command.Price);
                await catalogContext.SaveChangesAsync();
            }
            if(productItem.ItemFeatures != command.ItemFeatures)
            {
                productItem.EditFeatures(command.ItemFeatures);
                await catalogContext.SaveChangesAsync();
            }

            return Ok(new { ProductItemId = productItem.Id });
        }
    }
}
