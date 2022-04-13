using System.Reflection;
using dotmockator.core.definitions.attributes;

namespace dotmockator.core.definitions;

public class DefinitionField
{
    public string PropertyName => PropertyInfo.Name;

    public Boolean IsGroup => PropertyInfo.GetCustomAttributes<MockatorGroupAttribute>().Any();
    public Type? GroupType { get; private set; }
    public int Min { get; private set; }
    public int Max { get; private set; }

    public Boolean IsEmbedded => PropertyInfo.GetCustomAttributes<MockatorEmbeddedAttribute>().Any();

    public Type EmbeddedType { get; private set; }

    public Boolean IsDefinitionReuse => ReuseDefinition != null;
    
    public Definition? ReuseDefinition { get; private set; }
    
    public Boolean IsImplementation => PropertyInfo.GetCustomAttributes<MockatorImplementationTypeAttribute>().Any();

    public Type ImplementationType { get; private set; }

    public Boolean IsResolver => ResolverType != null || StaticFunc != null;
    public Func<object>? StaticFunc { get; }
    public Type? ResolverType { get; private set; }
    public Type? GeneratorType { get; private set; }
    public Boolean IsGenerator => GeneratorType != null;
    public PropertyInfo PropertyInfo { get; }

    private readonly Dictionary<Type, IConfiguration> _generatorConfigs = new();

    public static DefinitionField EmptyDefinitionField = new();

    public DefinitionField(PropertyInfo propertyInfo)
    {
        PropertyInfo = propertyInfo;
        ExtractResolverType();
        ExtractEmbeddedType();
        ExtractImplementationType();
        ExtractGroupType();

        if (!IsGroup)
        {
            ExtractGeneratorType();
            ExtractGeneratorConfigs();
        }
    }

    public DefinitionField(PropertyInfo propertyInfo, Type? generatorType, Type? resolverType,
        Func<object>? staticFunc,
        Definition? reuseDefinition,
        IEnumerable<IConfiguration> configurations)
    {
        PropertyInfo = propertyInfo;
        GeneratorType = generatorType;
        ResolverType = resolverType;
        StaticFunc = staticFunc;
        ReuseDefinition = reuseDefinition;

        foreach (var configuration in configurations)
        {
            _generatorConfigs.Add(configuration.GetType(), configuration);
        }
    }


    private DefinitionField()
    {
    }

    private void ExtractEmbeddedType()
    {
        if (IsEmbedded)
            EmbeddedType = PropertyInfo.PropertyType;
    }

    private void ExtractImplementationType()
    {
        if (IsImplementation)
            ImplementationType = PropertyInfo.GetCustomAttributes<MockatorImplementationTypeAttribute>().First()
                .ImplementationType;
    }

    private void ExtractResolverType()
    {
        if (PropertyInfo.GetCustomAttributes<DynamicMockatorFieldResolverAttribute>().Any())
        {
            var generatorTypeAttribute = GetAttributeConstructorArgument(0);
            if (generatorTypeAttribute.ArgumentType == typeof(Type))
            {
                ResolverType = (Type?) generatorTypeAttribute.Value;
            }
        }
    }

    private void ExtractGroupType()
    {
        if (!IsGroup)
            return;
        
        if (PropertyInfo.PropertyType.GetGenericArguments().Any())
        {
            GroupType = PropertyInfo.PropertyType.GetGenericArguments()[0];
            var minAttribute = GetAttributeConstructorArgument(0);
            var maxAttribute = GetAttributeConstructorArgument(1);
            Min = (int) (minAttribute.Value ?? 0);
            Max = (int) (maxAttribute.Value ?? 0);
        }
    }


    private void ExtractGeneratorType()
    {
        if (!IsEmbedded && !IsGroup)
        {
            var generatorTypeAttribute = GetAttributeConstructorArgument(0);
            if (generatorTypeAttribute.ArgumentType == typeof(Type))
            {
                GeneratorType = (Type?) generatorTypeAttribute.Value;
            }    
        }
    }

    private void ExtractGeneratorConfigs()
    {
        PropertyInfo.GetCustomAttributes<MockatorGeneratorConfigAttribute>().ToList().ForEach(
            gc => _generatorConfigs.TryAdd(gc.GetType(), (IConfiguration) gc));
    }

    private CustomAttributeTypedArgument GetAttributeConstructorArgument(int position)
    {
        foreach (var propertyInfoCustomAttribute in PropertyInfo.CustomAttributes)
        {
            return propertyInfoCustomAttribute.ConstructorArguments.ElementAt(position);
        }

        return default;
    }

    public T? GetGeneratorConfig<T>()
        where T : MockatorGeneratorConfigAttribute
    {
        if (_generatorConfigs.TryGetValue(typeof(T), out var generatorConfig))
        {
            return (T) generatorConfig;
        }

        ;
        return default;
    }
}