using Common.Domain.Base;

namespace Common.Domain.Language.Sales.ValueObjects
{
    public record PaymentSessionId : StronglyTypedId
    {
        public PaymentSessionId(Guid value) : base(value)
        {
        }
        public PaymentSessionId():base()
        {
            
        }
    }
}