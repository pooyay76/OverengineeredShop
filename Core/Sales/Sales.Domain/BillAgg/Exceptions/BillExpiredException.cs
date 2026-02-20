
namespace Sales.Domain.BillAgg.Exceptions
{
    public class BillExpiredException : Exception
    {
        private const string _message = "";
        public BillExpiredException() : base(_message)
        {
        }
    }
}