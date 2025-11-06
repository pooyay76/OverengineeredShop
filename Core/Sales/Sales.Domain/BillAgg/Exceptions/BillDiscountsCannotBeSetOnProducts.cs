using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Exceptions
{
    public class BillDiscountsCannotBeSetOnProducts : DomainException
    {
        private const string _message = "";
        public BillDiscountsCannotBeSetOnProducts() : base(_message)
        {
        }
    }
}
