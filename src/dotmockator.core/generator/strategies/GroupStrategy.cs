using dotmockator.core.definitions;

namespace dotmockator.core.generator.strategies;

public class GroupStrategy
{
    public static void HandleGroup<T>(T candidate, DefinitionField definitionField)
    {
        if (!definitionField.IsGroup)
            return;

        var result = Activator.CreateInstance(definitionField.PropertyInfo.PropertyType);
        var rnd = new Random();
        int amountFields = rnd.Next(definitionField.Min, definitionField.Max);
        for (int i = 0; i <= amountFields; i++)
        {
            result.GetType().GetMethod("Add")
                .Invoke(result,
                    new[]
                    {
                        MockatorGenerator.GenerateSingle(definitionField.GroupType.IsInterface
                            ? definitionField.ImplementationType
                            : definitionField.GroupType)
                    });
        }

        definitionField.PropertyInfo.SetValue(candidate, result);
    }
}