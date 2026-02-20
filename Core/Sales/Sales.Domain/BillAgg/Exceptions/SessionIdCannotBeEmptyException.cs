namespace Sales.Domain.BillAgg.Exceptions
{
    [Serializable]
    internal class SessionIdCannotBeEmptyException : Exception
    {
        private const string _message = "";



        public SessionIdCannotBeEmptyException() : base(_message)
        {
        }
    }
}