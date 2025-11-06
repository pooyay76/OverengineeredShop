using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Exceptions
{
    [Serializable]
    internal class SessionIdCannotBeEmptyException : DomainException
    {
        private const string _message = "";



        public SessionIdCannotBeEmptyException() : base(_message)
        {
        }
    }
}