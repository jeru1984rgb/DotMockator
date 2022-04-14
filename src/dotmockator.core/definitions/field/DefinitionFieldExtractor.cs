using System.Reflection;
using dotmockator.core.definitions.attributes;
using dotmockator.core.definitions.field;

namespace dotmockator.core.definitions;

public static class DefinitionFieldExtractor
{
    public static DefinitionField Extract(PropertyInfo propertyInfo)
    {
        var field = new DefinitionField(propertyInfo);

        ConfigureImplementationType(propertyInfo, field);
        ConfigureEmbeddedType(propertyInfo, field);
        ConfigureGroupType(propertyInfo, field);
        ConfigureGeneratorType(propertyInfo, field);
        ConfigureResolverType(propertyInfo, field);
        Configure(propertyInfo, field);

        return field;
    }

    private static void ConfigureEmbeddedType(PropertyInfo propertyInfo, DefinitionField field)
    {
        var embeddedAttribute = propertyInfo.GetCustomAttribute<MockatorEmbeddedAttribute>();
        if (embeddedAttribute != null)
        {
            field.WithEmbedded(propertyInfo.PropertyType);
        }
    }

    private static void ConfigureImplementationType(PropertyInfo propertyInfo, DefinitionField field)
    {
        var implementationAttribute = propertyInfo.GetCustomAttribute<MockatorImplementationTypeAttribute>();
        if (implementationAttribute != null)
        {
            field.WithImplementation(implementationAttribute.ImplementationType);
        }
    }

    private static void ConfigureGroupType(PropertyInfo propertyInfo, DefinitionField field)
    {
        var groupAttribute = propertyInfo.GetCustomAttribute<MockatorGroupAttribute>();
        if (groupAttribute != null)
        {
            field.WithGroup(propertyInfo.PropertyType.GetGenericArguments()[0]);
            field.WithConfigurations(new GroupMinMaxConfig(groupAttribute.Min, groupAttribute.Max));
        }
    }

    private static void ConfigureGeneratorType(PropertyInfo propertyInfo, DefinitionField field)
    {
        var mockatorAttribute = propertyInfo.GetCustomAttribute<MockatorFieldAttribute>();
        if (mockatorAttribute != null)
        {
            var generatorType = GetAttributeConstructorArgument(propertyInfo, 0).Value;
            if (generatorType != null)
                field.WithGenerator((Type) generatorType);
        }
    }

    private static void ConfigureResolverType(PropertyInfo propertyInfo, DefinitionField definitionField)
    {
        if (propertyInfo.GetCustomAttributes<DynamicMockatorFieldResolverAttribute>().Any())
        {
            var generatorTypeAttribute = GetAttributeConstructorArgument(propertyInfo, 0);
            if (generatorTypeAttribute.ArgumentType == typeof(Type) && generatorTypeAttribute.Value != null)
            {
                definitionField.WithResolver((Type) generatorTypeAttribute.Value);
            }
        }
    }
    
    private static void Configure(PropertyInfo propertyInfo, DefinitionField definitionField)
    {
        foreach (var mockatorGeneratorConfigAttribute in propertyInfo
                     .GetCustomAttributes<MockatorAttributeConfiguration>())
        {
            definitionField.WithConfigurations(mockatorGeneratorConfigAttribute);
        }
    }

    private static CustomAttributeTypedArgument GetAttributeConstructorArgument(PropertyInfo propertyInfo, int position)
    {
        foreach (var propertyInfoCustomAttribute in propertyInfo.CustomAttributes)
        {
            return propertyInfoCustomAttribute.ConstructorArguments.ElementAt(position);
        }

        return default;
    }
}