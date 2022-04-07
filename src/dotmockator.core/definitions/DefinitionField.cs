using System.Reflection;

namespace dotmockator.core.definitions;

public class DefinitionField
{
    public string PropertyName => PropertyInfo.Name;

    public FieldFunctionEnum FieldFunction { get; private set; }
    public PropertyInfo PropertyInfo { get; }

    public DefinitionField(PropertyInfo propertyInfo)
    {
        PropertyInfo = propertyInfo;
        ExtractFieldFunctionEnum();
    }

    
    private void ExtractFieldFunctionEnum()
    {
        foreach (var propertyInfoCustomAttribute in PropertyInfo.CustomAttributes)
        {
            foreach (var customAttributeTypedArgument in propertyInfoCustomAttribute.ConstructorArguments)
            {
                if (customAttributeTypedArgument.ArgumentType == typeof(FieldFunctionEnum))
                {
                    FieldFunction = (FieldFunctionEnum)(Int32) (customAttributeTypedArgument.Value ?? throw new InvalidOperationException());
                }
            }
        }

    }
}