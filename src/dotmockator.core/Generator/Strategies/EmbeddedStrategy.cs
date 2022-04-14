using DotMockator.Core.Definitions.Field;

namespace DotMockator.Core.Generator.Strategies;

public static class EmbeddedStrategy
{
    public static void HandleEmbedded<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.EmbeddedType.IsPresent || definitionField.GroupType.IsPresent)
            return;

        if (definitionField.ReuseDefinition.IsPresent)
        {
            definitionField.PropertyInfo.Value.SetValue(candidate,
                MockatorGenerator.GenerateSingle(definitionField.ReuseDefinition.Value));
        }
        else if (definitionField.ImplementationType.IsPresent)
        {
            definitionField.PropertyInfo.Value.SetValue(candidate,
                MockatorGenerator.GenerateSingle(definitionField.ImplementationType.Value));
        }
        else if (definitionField.EmbeddedType.IsPresent)
        {
            definitionField.PropertyInfo.Value.SetValue(candidate,
                MockatorGenerator.GenerateSingle(definitionField.EmbeddedType.Value));
        }
    }
}