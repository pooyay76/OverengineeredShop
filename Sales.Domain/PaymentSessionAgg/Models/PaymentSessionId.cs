using Sales.Domain._common.Base;

namespace Sales.Domain.PaymentSessionAgg.Models
{
    public class PaymentSessionId : StronglyTypedId
    {
        public PaymentSessionId(Guid value) : base(value)
        {
        }
    }
}