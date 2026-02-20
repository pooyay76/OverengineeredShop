namespace Sales.Domain.OrderAgg.Exceptions
{
    public class BillDoesNotBelongToUserException : Exception
    {
        private const string _message = "";
        public BillDoesNotBelongToUserException() : base(_message)
        {
        }
    }
}