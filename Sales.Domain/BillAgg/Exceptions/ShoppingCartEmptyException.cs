using Sales.Domain._common.Base;

namespace Sales.Domain.BillAgg.Exceptions
{
    public class ShoppingCartEmptyException : DomainException
    {
        private const string _message = "";
        public ShoppingCartEmptyException() : base(_message)
        {
        }
    }
}
