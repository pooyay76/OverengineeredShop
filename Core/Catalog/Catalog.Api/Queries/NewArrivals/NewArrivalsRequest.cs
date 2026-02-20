using Common.Application.DTOs;
using MediatR;

namespace Catalog.Api.Queries.NewArrivals
{
    public class NewArrivalsRequest : PageRequest,IRequest<NewArrivalsResponse>
    {

    }
}
