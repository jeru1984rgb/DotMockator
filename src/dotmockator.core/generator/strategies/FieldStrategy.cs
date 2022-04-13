using dotmockator.core.definitions;

namespace dotmockator.core.generator.strategies;

public class FieldStrategy
{
    public static void HandleField<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.IsGenerator)
            return;

        object? generator = Activator.CreateInstance(definitionField.GeneratorType);

        if (generator is IGenerator)
        {
            definitionField.PropertyInfo.SetValue(candidate, ((IGenerator) generator).Generate(definitionField));
        }
    }
}