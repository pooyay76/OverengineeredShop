namespace Common.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(List<string> messages) : base(string.Join("\n",messages))
        {
        }
    }
}
