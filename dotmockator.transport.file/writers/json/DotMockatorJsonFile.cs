using Newtonsoft.Json;

namespace dotmockator.transport.file.writers.json;

public class DotMockatorJsonFile<T> : AbstractFileWriter<T>
{
    public JsonSerializerSettings JsonSerializerSettings { get; private set; }
    public DotMockatorJsonFile(string file, string path) : base(file, path)
    {
    }

    public override string DeserializeMocks()
    {
        var jsonConverters = new List<JsonConverter>();

        foreach (var definitionField in Definition.Fields.Where(df => df.IsImplementation))
        {
            jsonConverters.Add(new AbstractJsonConverter(definitionField.IsEmbedded ? definitionField.EmbeddedType : definitionField.GroupType,
                definitionField.ImplementationType));
        }

        JsonSerializerSettings = new JsonSerializerSettings();
        JsonSerializerSettings.Converters = jsonConverters;
        JsonSerializerSettings.Formatting = Formatting.Indented;
        return JsonConvert.SerializeObject(Mocks, JsonSerializerSettings);
    }
}