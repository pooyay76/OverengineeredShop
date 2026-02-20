using Catalog.Api.Commands;
using Catalog.Api.Contracts;
using Catalog.Api.Data;
using Catalog.Api.Models;
using Catalog.Api.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly CatalogContext catalogContext;
    private readonly IMediaClient mediaClientExternalServices;
    private readonly IConfiguration configuration;
    private readonly HttpContext httpContext;
    public ProductsController(CatalogContext catalogContext, IMediaClient mediaClientExternalServices, IConfiguration configuration, HttpContext httpContext)
    {
        this.catalogContext = catalogContext;
        this.mediaClientExternalServices = mediaClientExternalServices;
        this.configuration = configuration;
        this.httpContext = httpContext;
    }

    [HttpPost("create", Name = "CreateProduct")]
    public async Task<IActionResult> Create([FromForm]CreateProductCommand command)
    {
        var fileExtension = Path.GetExtension(command.ProductPicture.FileName).TrimStart('.');
        using var fileStream = command.ProductPicture.OpenReadStream();
        using var memory = new MemoryStream();
        byte[] fileData;
        fileStream.CopyTo(memory);
        fileData= memory.ToArray();
        var result = await mediaClientExternalServices.UploadMediaAsync(fileExtension, fileData);
        
        Product product = new(command.Name, command.Description,result.MediaPath);

        await catalogContext.Products.AddAsync(product);
        await catalogContext.SaveChangesAsync();
        return Ok(product.Id);
    }

    [HttpPut("edit", Name = "EditProduct")]
    public async Task<IActionResult> EditProductAsync(EditProductCommand command)
    {

        Product product = catalogContext.Products.Find(command.Id);

        if (product == null)
            throw new KeyNotFoundException();


        product.Edit(command.Name, command.Description,command.PictureMediaAddress);


        catalogContext.Products.Update(product);
        await catalogContext.SaveChangesAsync();
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
        if(httpContext.+)
        Product product = catalogContext.Products.Find(id);
        if (product == null)
            throw new KeyNotFoundException();

        var mediaHostUrl = configuration.GetSection("External")["MediaHost"];
        var result = new ProductDTO { Id = product.Id, Name = product.Name, Description = product.Description,
        PictureUrl = $"{mediaHostUrl}/Medias/{product.PictureMediaAddress}"};
        return Ok(result);


    }


    [HttpGet("getAll", Name = "GetProducts")]
    public async Task<ActionResult<List<ProductDTO>>> GetProductsAsync()
    {

        var mediaHostUrl = configuration.GetSection("External")["MediaHost"];

        return Ok(await catalogContext.Products.Include(x=>x.ProductItems)
            .Select(x => new ProductDTO
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                IsPublished = x.IsPublished,
                CategoryId = x.CategoryId,
                PictureUrl = $"{mediaHostUrl}/Medias/{x.PictureMediaAddress}",
                Price = x.IsPublished ? x.ProductItems.Min(y => y.Price.Amount).ToString() : "",
                ItemsCount = x.ProductItemsCount
            }).ToListAsync());


    }
}
