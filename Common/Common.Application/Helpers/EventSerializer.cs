using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Feedback.ValueObjects;
using System.Text.Json;

namespace Common.Application.Helpers
{
    public static class EventSerializer
    {
        public static JsonSerializerOptions Options = new(){
            Converters = {
                        new MoneyJsonConverter(),
                        new StronglyTypedIdJsonConverter<ProductItemId>(),
                        new StronglyTypedIdJsonConverter<ProductCategoryId>(),
                        new StronglyTypedIdJsonConverter<ProductId>(),
                        new StronglyTypedIdJsonConverter<CommentId>()
                        }

                    };
        public static T ToObject<T>(this string eventData) where T : EventBase
        {
            return JsonSerializer.Deserialize<T>(eventData,Options);
        }
        public static string ToJson(this object eventData)
        {
            return JsonSerializer.Serialize(eventData, Options);
        }
    }
}
