using Sales.Domain._common.Base;

namespace Sales.Domain.OrderAgg.Exceptions
{
    public class BillDoesNotBelongToUserException : DomainException
    {
        private const string _message = "";
        public BillDoesNotBelongToUserException() : base(_message)
        {
        }
    }
}