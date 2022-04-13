using System.Reflection;

namespace dotmockator.core.definitions.builder;

public interface IDefinitionBuilder
{
    Definition Build();

    IDefinitionBuilder HavingField(string propertyName, Action<IDefinitionFieldBuilder> configure);
}

public interface IDefinitionFieldBuilder
{
    DefinitionField Build();
    IDefinitionFieldBuilder WithGenerator(Type generatorType);
    IDefinitionFieldBuilder WithResolver(Type resolverType);
    IDefinitionFieldBuilder WithStatic(Func<object> staticFunc);
    IDefinitionFieldBuilder WithConfiguration(IConfiguration configuration);

    IDefinitionFieldBuilder AsGroup(int min, int max);
    IDefinitionFieldBuilder UseDefinition(Definition definition);
}

public class DefinitionFieldBuilder : IDefinitionFieldBuilder
{
    private PropertyInfo _propertyInfo;
    
    private Type? _generatorType;

    private Type? _resolverType;

    private Definition? _defintion;
    
    private Func<object>? _staticFunc;
    
    private List<IConfiguration> _configurations = new ();

    private int _min = 0;
    private int _max = 0;
    
    public DefinitionFieldBuilder(Type mockedType, string propertyName)
    {
        _propertyInfo = mockedType.GetProperty(propertyName) ?? throw new InvalidOperationException();
    }

    public DefinitionField Build()
    {
        return new DefinitionField(_propertyInfo, _generatorType, _resolverType, _staticFunc, _defintion, _configurations);
    }

    public IDefinitionFieldBuilder WithGenerator(Type generatorType)
    {
        _generatorType = generatorType;
        return this;
    }

    public IDefinitionFieldBuilder WithResolver(Type resolverType)
    {
        _resolverType = resolverType;
        return this;
    }

    public IDefinitionFieldBuilder WithStatic(Func<object> staticFunc)
    {
        _staticFunc = staticFunc;
        return this;
    }

    public IDefinitionFieldBuilder WithConfiguration(IConfiguration configuration)
    {
        _configurations.Add(configuration);
        return this;
    }

    public IDefinitionFieldBuilder AsGroup(int min, int max)
    {
        _min = min;
        _max = max;
        return this;
    }

    public IDefinitionFieldBuilder UseDefinition(Definition definition)
    {
        _defintion = definition;
        return this;
    }
}

public class DefinitionBuilder<T> : IDefinitionBuilder
{
    private Definition _definition = new (typeof(T));

    public Definition Build() => _definition;

    public IDefinitionBuilder HavingField(string propertyName, Action<IDefinitionFieldBuilder> configure)
    {
        var fieldBuilder = new DefinitionFieldBuilder(typeof(T), propertyName);
        configure(fieldBuilder);
        _definition.AddField(fieldBuilder.Build());
        return this;
    }
    
}

