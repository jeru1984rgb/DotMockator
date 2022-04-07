using System.Reflection;

namespace dotmockator.core.definitions;

public class DefinitionExtractor
{
    public static Definition ExtractDefinition<T>()
    {
        var definition = new Definition();

        var mockedProperties = typeof(T).GetProperties().Where(IsMockatorField);
        foreach (var mockedProperty in mockedProperties)
        {
            definition.AddField(new DefinitionField(mockedProperty));
        }

        return definition;
    }

    private static bool IsMockatorField(PropertyInfo property)
    {
        return property.CustomAttributes.Any(propertyInfoCustomAttribute =>
            propertyInfoCustomAttribute.AttributeType == typeof(MockatorFieldAttribute));
    }
}