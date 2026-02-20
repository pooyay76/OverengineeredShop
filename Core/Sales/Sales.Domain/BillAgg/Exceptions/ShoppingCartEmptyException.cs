
namespace Sales.Domain.BillAgg.Exceptions
{
    public class ShoppingCartEmptyException : Exception
    {
        private const string _message = "";
        public ShoppingCartEmptyException() : base(_message)
        {
        }
    }
}
