using dotmockator.core.definitions.field;

namespace dotmockator.core.generator.strategies;

public static class GroupStrategy
{
    public static void HandleGroup<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.GroupType.IsPresent)
            return;

        var result = Activator.CreateInstance(definitionField.PropertyInfo.Value.PropertyType);
        var rnd = new Random();
        var minMaxConfig = definitionField.GetConfiguration<GroupMinMaxConfig>();
        int amountFields = rnd.Next(minMaxConfig.Min, minMaxConfig.Max);
        for (int i = 0; i <= amountFields; i++)
        {
            object? entry = null;
            if (definitionField.ReuseDefinition.IsPresent)
            {
                entry = MockatorGenerator.GenerateSingle(definitionField.ReuseDefinition.Value);
            }
            else
            {
                entry = MockatorGenerator.GenerateSingle(definitionField.GroupType.Value.IsInterface
                    ? definitionField.ImplementationType.Value
                    : definitionField.GroupType.Value);
            }

            if (result != null)
            {
                result.GetType().GetMethod("Add")
                    ?.Invoke(result,
                        new[]
                        {
                            entry
                        });    
            }
            
        }

        definitionField.PropertyInfo.Value.SetValue(candidate, result);
    }
}