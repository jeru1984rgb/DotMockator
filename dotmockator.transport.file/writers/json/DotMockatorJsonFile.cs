using Newtonsoft.Json;

namespace dotmockator.transport.file.writers.json;

public class DotMockatorJsonFile<T> : AbstractFileWriter<T>
{
    public JsonSerializerSettings JsonSerializerSettings { get; private set; } = new ();

    public DotMockatorJsonFile(string file, string path) : base(file, path)
    {
    }

    public override string DeserializeMocks()
    {
        var jsonConverters = new List<JsonConverter>();

        foreach (var definitionField in Definition.Fields.Where(df => df.ImplementationType.IsPresent))
        {
            jsonConverters.Add(new AbstractJsonConverter(
                definitionField.EmbeddedType.IsPresent
                    ? definitionField.EmbeddedType.Value
                    : definitionField.GroupType.Value,
                definitionField.ImplementationType.Value));
        }

        JsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = jsonConverters,
            Formatting = Formatting.Indented
        };
        return JsonConvert.SerializeObject(Mocks, JsonSerializerSettings);
    }
}