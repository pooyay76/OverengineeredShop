
namespace Sales.Domain.BillAgg.Exceptions
{
    public class BillHasAlreadyBeenPaidException : Exception
    {
        private const string _message = "";
        public BillHasAlreadyBeenPaidException() : base(_message)
        {
        }
    }
}