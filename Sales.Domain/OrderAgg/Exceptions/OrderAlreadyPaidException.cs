using Sales.Domain.Common.Base;

namespace Sales.Domain.OrderAgg.Exceptions
{
    public class OrderAlreadyPaidException : DomainException
    {
        private const string _message = "Order has already been paid";
        public OrderAlreadyPaidException() : base(_message)
        {
        }
    }
}
