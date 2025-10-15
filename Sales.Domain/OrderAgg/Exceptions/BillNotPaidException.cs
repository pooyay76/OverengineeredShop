using Sales.Domain._common.Base;

namespace Sales.Domain.OrderAgg.Exceptions
{
    public class BillNotPaidException : DomainException
    {
        private const string _message = "";
        public BillNotPaidException() : base(_message)
        {
        }
    }
}