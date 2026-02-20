
namespace Sales.Domain.OrderAgg.Exceptions
{
    public class BillNotPaidException : Exception
    {
        private const string _message = "";
        public BillNotPaidException() : base(_message)
        {
        }
    }
}