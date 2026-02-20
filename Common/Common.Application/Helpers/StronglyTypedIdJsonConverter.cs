using Common.Domain.Base;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

public class StronglyTypedIdJsonConverter<T> : JsonConverter<T> where T:StronglyTypedId
{ 
    private static readonly Type TargetType = typeof(T);

    private static readonly PropertyInfo? ValueProperty = 
        TargetType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance); 
    private static readonly ConstructorInfo? GuidCtor = TargetType.GetConstructor(new[] { typeof(Guid) });
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) 
    { 
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Expected string for strongly-typed ID {TargetType.Name}");
        var stringValue = reader.GetString();
        if (!Guid.TryParse(stringValue, out var guid))
            throw new JsonException($"Invalid GUID value for {TargetType.Name}: {stringValue}");
        if (GuidCtor is null) 
            throw new InvalidOperationException($"{TargetType.Name} must have a constructor with signature .ctor(Guid)");
        return (T)GuidCtor.Invoke(new object[] { guid }); } 
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) 
    {
        if (ValueProperty is null)
            throw new InvalidOperationException($"{TargetType.Name} must have a public property named 'Value'");
        var guid = (Guid)ValueProperty.GetValue(value)!; 
        writer.WriteStringValue(guid); 
    }

}