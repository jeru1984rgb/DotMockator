using System.Reflection;
using DotMockator.Core.Definitions.Attributes;
using DotMockator.Core.Definitions.Field;

namespace DotMockator.Core.Definitions;

public class DefinitionExtractor
{
    public static Definition ExtractDefinition(Type definitionClass)
    {
        var definition = new Definition(definitionClass);
        IEnumerable<PropertyInfo> properties = new List<PropertyInfo>();
        properties = properties.Concat(definitionClass.GetProperties().Where(IsEmbedded));
        properties = properties.Concat(definitionClass.GetProperties().Where(IsResolver));
        properties = properties.Concat(definitionClass.GetProperties().Where(IsMockatorGroup));
        properties = properties.Concat(definitionClass.GetProperties().Where(IsMockatorField));
        foreach (var mockedProperty in properties)
        {
            definition.AddField(DefinitionFieldExtractor.Extract(mockedProperty));
        }

        return definition;
    }

    

    private static bool IsEmbedded(PropertyInfo property)
    {
        return property.CustomAttributes.Any(propertyInfoCustomAttribute =>
            propertyInfoCustomAttribute.AttributeType == typeof(MockatorEmbeddedAttribute));
    }
    
    private static bool IsResolver(PropertyInfo property)
    {
        return property.CustomAttributes.Any(propertyInfoCustomAttribute =>
            propertyInfoCustomAttribute.AttributeType == typeof(DynamicMockatorFieldResolverAttribute));
    }
    private static bool IsMockatorGroup(PropertyInfo property)
    {
        return property.CustomAttributes.Any(propertyInfoCustomAttribute =>
            propertyInfoCustomAttribute.AttributeType == typeof(MockatorGroupAttribute));
    }

    private static bool IsMockatorField(PropertyInfo property)
    {
        return property.CustomAttributes.Any(propertyInfoCustomAttribute =>
            propertyInfoCustomAttribute.AttributeType == typeof(MockatorFieldAttribute));
    }
}