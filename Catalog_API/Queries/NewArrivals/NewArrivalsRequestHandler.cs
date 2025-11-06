using Catalog.Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Api.Queries.NewArrivals
{
    public class NewArrivalsRequestHandler : IRequestHandler<NewArrivalsRequest, NewArrivalsResponse>
    {
        private readonly CatalogContext catalogContext;

        public NewArrivalsRequestHandler(CatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
        }

        public async Task<NewArrivalsResponse> Handle(NewArrivalsRequest request, CancellationToken cancellationToken)
        {
            int pageSize = Convert.ToInt32(request.PageSize > 50 ? 50 : request.PageSize);
            int skipItems = Convert.ToInt32((request.CurrentPage-1)*request.PageSize);

            //change false to true later
            var totalCount = catalogContext.Products.Where(x => x.IsPublished == false).Count();
            var productData =await catalogContext.Products.Where(x => x.IsPublished == false).OrderByDescending(x => x.PublishedAt)
              .Skip(skipItems).Take(pageSize)
              .Include(x => x.Items).Select(x=>new NewArrivalsProductDto
              {
                  ProductId = x.Id,
                  ProductName = x.Name,
                  ProductPrice = x.IsPublished? x.Items.Min(x=>x.Price.Amount).ToString():"",
                  ProductThumbnailUrl = x.PictureMediaAddress
              })
              .ToListAsync(cancellationToken);

            var response = new NewArrivalsResponse(productData, totalCount,pageSize,request.CurrentPage);

            return response;
        }
    }
}
