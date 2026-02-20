using Catalog.Api.Commands;
using Catalog.Api.Data;
using Catalog.Api.Models;
using Catalog.Api.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly CatalogContext catalogContext;

        public ProductCategoriesController(CatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateProductCategoryCommand command)
        {
            try
            {
                ProductCategory category = new(command.Title);
                await catalogContext.ProductCategories.AddAsync(category);
                await catalogContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("edit")]
        public async Task<IActionResult> EditAsync(EditProductCategoryCommand command)
        {
            try
            {

                ProductCategory category = catalogContext.ProductCategories.Find(command.Id);
                if (category == null)
                    throw new KeyNotFoundException();
                category.Edit(command.Title);
                await catalogContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound();
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<ProductCategoryDTO> GetById(long id)
        {
            try
            {

                ProductCategory category = catalogContext.ProductCategories.Find(id);
                if (category == null)
                    throw new KeyNotFoundException();
                var result = new ProductCategoryDTO
                {
                    Id = category.Id,
                    ProductsCount = category.Products.Count(),
                    Title = category.Title
                };
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("getAll")]
        public ActionResult<List<ProductCategoryDTO>> GetAll()
        {
            List<ProductCategoryDTO> categories = catalogContext.ProductCategories.Include(x => x.Products)
                .Select(x => new ProductCategoryDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    ProductsCount = x.Products.Count()
                }).ToList();

            return Ok(categories);
        }


    }
}
