namespace Sales.Domain._common.ValueObjects
{
    public record Money
    {
        private decimal Amount { get; init; }

        public Money(decimal amount)
        {
            Amount = amount;
        }
        public decimal GetValue()
        {
            return Amount;
        }
        public static Money operator +(Money a, Money b)
        {
            return new Money(a.Amount + b.Amount);
        }
        public static Money operator -(Money a, Money b)
        {
            return new Money(a.Amount - b.Amount);
        }

        public static Money operator *(Money a, Money b)
        {
            return new Money(a.Amount * b.Amount);
        }
        public static Money operator /(Money a, Money b)
        {
            return new Money(a.Amount / b.Amount);
        }
    }
}
