
namespace Sales.Domain.BillAgg.Exceptions
{
    public class CannotChangeBillStatusAfterItIsSetException : Exception
    {
        private const string _message = "";
        public CannotChangeBillStatusAfterItIsSetException() : base(_message)
        {
        }
    }
}