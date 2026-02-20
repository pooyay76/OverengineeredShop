using System.Globalization;

namespace Common.Domain.Language.Global.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }



        public Money(decimal amount, string currency="")
        {
            Amount = amount;
            Currency = currency;
        }

        public override string ToString()
        {
            return $"{Amount.ToString("C", new CultureInfo(Currency))}";
        }

        public decimal GetValue()
        {
            return Amount;
        }
        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot add price with different currencies.");
            return new Money(a.Amount + b.Amount, a.Currency);
        }


        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot add price with different currencies.");
            return new Money(a.Amount - b.Amount, a.Currency);
        }
    }
}
