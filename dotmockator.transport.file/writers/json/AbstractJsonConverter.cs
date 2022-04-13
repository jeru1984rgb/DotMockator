using Newtonsoft.Json;

namespace dotmockator.transport.file.writers.json;

public class AbstractJsonConverter 
    : JsonConverter 
{
    public Type AbstractType { get; }
    public Type RealType { get; }
    public AbstractJsonConverter(Type abstractType, Type realType)
    {
        AbstractType = abstractType;
        RealType = realType;
    }

    public override Boolean CanConvert(Type objectType)
    {
        var result = objectType == AbstractType;
        if (result)
            return result;
        return result;
    }

    public override Object ReadJson(JsonReader reader, Type type, Object value, JsonSerializer jser)
        => jser.Deserialize(reader, RealType);

    public override void WriteJson(JsonWriter writer, Object value, JsonSerializer jser)
        => jser.Serialize(writer, value, RealType);
}