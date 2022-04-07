using dotmockator.core.definitions;

namespace dotmockator.core.generator;

public class DotMockatorGenerator
{
    public T GenerateSingle<T>()
    {
        var candidate = Activator.CreateInstance<T>();
        var definition = GetDefinition<T>();

        foreach (var definitionField in definition.Fields)
        {
            IGenerator generator = GeneratorRegister.Instance.GetGenerator(definitionField.FieldFunction);
            definitionField.PropertyInfo.SetValue(candidate, generator.Generate());
        }

        return candidate;
    }

    private Definition GetDefinition<T>()
    {
        return DefinitionExtractor.ExtractDefinition<T>();
    }
}