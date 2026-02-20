using Common.Application.DTOs;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Catalog.Api.Queries.NewArrivals
{
    public class NewArrivalsProductDto
    {
        public ProductId ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductThumbnailUrl { get; set; }
        public string ProductPrice { get; set; }

    }
    public class NewArrivalsResponse : PageResult<NewArrivalsProductDto>
    {
        public NewArrivalsResponse(ICollection<NewArrivalsProductDto> dataCollection, 
            int totalItemsCount, int pageSize = 25, int currentPage = 0) : base(dataCollection, totalItemsCount, pageSize, currentPage)
        {
        }
    }
}
