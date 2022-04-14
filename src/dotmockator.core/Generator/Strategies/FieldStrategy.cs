using DotMockator.Core.Definitions.Field;

namespace DotMockator.Core.Generator.Strategies;

public static class FieldStrategy
{
    public static void HandleField<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.GeneratorType.IsPresent)
            return;

        object? generator = Activator.CreateInstance(definitionField.GeneratorType.Value);

        if (generator is IGenerator)
        {
            definitionField.PropertyInfo.Value.SetValue(candidate, ((IGenerator) generator).Generate(definitionField));
        }
    }
}