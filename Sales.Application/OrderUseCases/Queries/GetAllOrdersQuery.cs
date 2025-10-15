using MediatR;
using Sales.Application.DTOs;
using Sales.Domain.OrderAgg.Models;

namespace Sales.Application.OrderUseCases.Queries
{
    public class GetAllOrdersQueryRequest : IRequest<QueryCollectionResponse<GetAllOrdersResponseDto>>
    {
    }
    public class GetAllOrdersResponseDto
    {
        public OrderId OrderId { get; set; }
        public string CustomerFullName { get; set; }
        public int OrderCode { get; set; }
        public long MyProperty { get; set; }
    }
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, QueryCollectionResponse<GetAllOrdersResponseDto>>
    {
        public Task<QueryCollectionResponse<GetAllOrdersResponseDto>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
