using Common.Domain.Language.Enums;
namespace Sales.Api.Controllers
{
    public partial class BillController
    {
        public record ProceedToPaymentRequest
        {
            public Guid CustomerId;
            public string ReceiverFirstName { get; init; }
            public string ReceiverLastName { get; init; }


            public ShippingType ShippingType { get; init; }
            public string Country { get; init; }
            public string Province { get; init; }
            public string City { get; init; }
            public string Address { get; init; }
            public string PostalCode { get; init; }
            public string ContactPhoneNumber { get; init; }

        }
    }
}
