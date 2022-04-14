using System.Reflection;

namespace DotMockator.Core.Definitions.Field;

public class DefinitionField
{
    
    public static readonly DefinitionField EmptyDefinitionField = new ();

    public OptionalDefinitionFieldParam<Type> ImplementationType { get; private set; } =
        new(null);

    public OptionalDefinitionFieldParam<Type> GroupType { get; private set; } =
        new(null);

    public OptionalDefinitionFieldParam<Type> ResolverType { get; private set; } =
        new(null);

    public OptionalDefinitionFieldParam<Type> GeneratorType { get; private set; } =
        new(null);

    public OptionalDefinitionFieldParam<Type> EmbeddedType { get; private set; } = new(null);

    public OptionalDefinitionFieldParam<Func<object>> StaticFunc { get; private set; } = new(null);
    public OptionalDefinitionFieldParam<Definition> ReuseDefinition { get; private set; } = new(null);


    public OptionalDefinitionFieldParam<PropertyInfo> PropertyInfo { get; private set; } = new(null);
    public string PropertyName => PropertyInfo.Value.Name;

    private readonly Dictionary<Type, IMockatorConfiguration> _generatorConfigs = new();
    
    public DefinitionField(PropertyInfo propertyInfo)
    {
        PropertyInfo = new OptionalDefinitionFieldParam<PropertyInfo>(propertyInfo);
    }

    private DefinitionField()
    {
        
    }

    public DefinitionField WithImplementation(Type implementationType)
    {
        ImplementationType = new OptionalDefinitionFieldParam<Type>(implementationType);
        return this;
    }

    public DefinitionField WithGroup(Type groupType)
    {
        GroupType = new OptionalDefinitionFieldParam<Type>(groupType);
        return this;
    }

    public DefinitionField WithResolver(Type resolverType)
    {
        ResolverType = new OptionalDefinitionFieldParam<Type>(resolverType);
        return this;
    }

    public DefinitionField WithGenerator(Type generatorType)
    {
        GeneratorType = new OptionalDefinitionFieldParam<Type>(generatorType);
        return this;
    }

    public DefinitionField WithEmbedded(Type embeddedType)
    {
        EmbeddedType = new OptionalDefinitionFieldParam<Type>(embeddedType);
        return this;
    }
    
    public DefinitionField WithConfigurations(params IMockatorConfiguration[] configurations)
    {
        foreach (var configuration in configurations)
        {
            _generatorConfigs.TryAdd(configuration.GetType(), configuration);
        }

        return this;
    }

    public DefinitionField WithReuseDefinition(Definition reuseDefinition)
    {
        ReuseDefinition = new OptionalDefinitionFieldParam<Definition>(reuseDefinition);
        return this;
    }

    public T GetConfiguration<T>()
        where T : IMockatorConfiguration
    {
        if (_generatorConfigs.TryGetValue(typeof(T), out var generatorConfig))
        {
            return (T) generatorConfig;
        }

        var configuration = Activator.CreateInstance<T>();
        configuration.DefaultConfiguration();

        return configuration;
    }
}