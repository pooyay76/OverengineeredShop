using Catalog_API.Contracts.Commands;
using Catalog_API.Contracts.Queries;
using Catalog_API.Data;
using Catalog_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly CatalogContext catalogContext;
    public ProductsController(CatalogContext catalogContext)
    {
        this.catalogContext = catalogContext;
    }

    [HttpPost("create", Name = "CreateProduct")]
    public IActionResult Create(CreateProductCommand command)
    {

        Product product = new(command.Name, command.Description);
        catalogContext.Products.Add(product);
        catalogContext.SaveChanges();
        return Ok();

    }


    [HttpPut("assignToProductCategory")]
    public IActionResult AssignToProductCategory(AssignProductToProductCategoryCommand command)
    {

        var product = catalogContext.Products.Find(command.ProductId);
        if (product == null)
            throw new KeyNotFoundException();
        product.AssignToProductCategory(command.ProductCategoryId);
        catalogContext.SaveChanges();
        return Ok();


    }


    [HttpGet("{id}", Name = "GetById")]
    public ActionResult<ProductDTO> GetById(long id)
    {

        Product product = catalogContext.Products.Find(id);
        if (product == null)
            throw new KeyNotFoundException();
        var result = new ProductDTO { Id = product.Id, Name = product.Name, Description = product.Description };
        return Ok(result);


    }

    [HttpPut("edit", Name = "EditProduct")]
    public IActionResult EditProduct(EditProductCommand command)
    {


        Product product = catalogContext.Products.Find(command.Id);
        if (product == null)
            throw new KeyNotFoundException();
        product.Edit(command.Name, command.Description);
        catalogContext.Products.Update(product);
        catalogContext.SaveChanges();
        return Ok();

    }

    [HttpGet("getAll", Name = "GetProducts")]
    public ActionResult<List<ProductDTO>> GetProducts()
    {
        return Ok(catalogContext.Products
            .Select(x => new ProductDTO
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                IsDraftMode = x.IsDraftMode,
                CategoryId = x.ProductCategoryId
            }).ToList());
    }
}
