using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Exceptions
{
    public class BillExpiredException : DomainException
    {
        private const string _message = "";
        public BillExpiredException() : base(_message)
        {
        }
    }
}