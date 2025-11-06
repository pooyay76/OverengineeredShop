using System.Globalization;

namespace Catalog.Api.Models.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }
        public Money()
        {

        }

        public Money(decimal amount,string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public override string ToString()
        {
            return $"{Amount.ToString("C", new CultureInfo(Currency))}";
        }
      
    }
}
