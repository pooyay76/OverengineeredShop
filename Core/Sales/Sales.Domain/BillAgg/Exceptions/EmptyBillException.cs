
namespace Sales.Domain.BillAgg.Exceptions
{
    public class EmptyBillException : Exception
    {
        private const string _message = "";
        public EmptyBillException() : base(_message)
        {
        }
    }
}
