using Common.Domain.Language.Global.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Application.Helpers
{
    public class MoneyJsonConverter : JsonConverter<Money>
    {
        public override Money Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var amount = root.GetProperty("Amount").GetDecimal();
                var currency = root.GetProperty("Currency").GetString();
                return new Money(amount, currency);
            }
        }

        public override void Write(Utf8JsonWriter writer, Money value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("Amount", value.Amount);
            writer.WriteString("Currency", value.Currency);
            writer.WriteEndObject();
        }
    }
}
