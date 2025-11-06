using Catalog.Api.Contracts.Abstracts;
using MediatR;

namespace Catalog.Api.Queries.NewArrivals
{
    public class NewArrivalsRequest : PageRequest,IRequest<NewArrivalsResponse>
    {

    }
}
