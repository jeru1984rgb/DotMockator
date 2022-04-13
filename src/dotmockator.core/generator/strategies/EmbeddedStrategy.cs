using dotmockator.core.definitions;

namespace dotmockator.core.generator.strategies;

public class EmbeddedStrategy
{
    public static void HandleEmbedded<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.IsEmbedded && !definitionField.IsDefinitionReuse)
            return;

        
        if (definitionField.IsDefinitionReuse)
        {
            definitionField.PropertyInfo.SetValue(candidate, MockatorGenerator.GenerateSingle<T>(definitionField.ReuseDefinition));
        }
        else if (definitionField.EmbeddedType.IsInterface)
        {
            definitionField.PropertyInfo.SetValue(candidate, MockatorGenerator.GenerateSingle(definitionField.ImplementationType));
        }
        else
        {
            definitionField.PropertyInfo.SetValue(candidate, MockatorGenerator.GenerateSingle(definitionField.EmbeddedType));
        }
    }
}