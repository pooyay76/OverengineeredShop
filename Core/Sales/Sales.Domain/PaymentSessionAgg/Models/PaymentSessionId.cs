using Sales.Domain.Common.Base;

namespace Sales.Domain.PaymentSessionAgg.Models
{
    public record PaymentSessionId : StronglyTypedId
    {
        public PaymentSessionId(Guid value) : base(value)
        {
        }
    }
}