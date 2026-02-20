
namespace Sales.Domain.OrderAgg.Exceptions
{
    public class OrderAlreadyPaidException : Exception
    {
        private const string _message = "Order has already been paid";
        public OrderAlreadyPaidException() : base(_message)
        {
        }
    }
}
