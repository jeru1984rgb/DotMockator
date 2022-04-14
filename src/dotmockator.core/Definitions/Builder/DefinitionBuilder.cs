using System.Reflection;
using DotMockator.Core.Definitions.Field;

namespace DotMockator.Core.Definitions.Builder;

public interface IDefinitionBuilder
{
    Definition Build();

    IDefinitionBuilder HavingField(string propertyName, Action<IDefinitionFieldBuilder> configure);
    IDefinitionBuilder HavingGroup(string propertyName, Definition groupDefinition);
    IDefinitionBuilder HavingGroup(string propertyName, Definition groupDefinition, int min, int max);
}

public interface IDefinitionFieldBuilder
{
    DefinitionField Build();
    IDefinitionFieldBuilder WithGenerator(Type generatorType);
    IDefinitionFieldBuilder WithResolver(Type resolverType);
    IDefinitionFieldBuilder WithConfiguration(IMockatorConfiguration configuration);

    IDefinitionFieldBuilder AsGroup(int min, int max);
    IDefinitionFieldBuilder UseDefinition(Definition definition);
}

public class DefinitionFieldBuilder : IDefinitionFieldBuilder
{
    private PropertyInfo _propertyInfo;
    
    private Type? _generatorType;

    private Type? _resolverType;

    private Definition? _defintion;
    
    private List<IMockatorConfiguration> _configurations = new ();

    private int _min;
    private int _max;
    private Boolean _isGroup;
    
    public DefinitionFieldBuilder(Type mockedType, string propertyName)
    {
        _propertyInfo = mockedType.GetProperty(propertyName) ?? throw new InvalidOperationException();
    }

    public DefinitionField Build()
    {
        var field = new DefinitionField(_propertyInfo);
        if (_generatorType != null)
            field.WithGenerator(_generatorType);
        if (_resolverType != null)
            field.WithResolver(_resolverType);
        if (_configurations.Any())
            field.WithConfigurations(_configurations.ToArray());
        if (_defintion != null)
        {
            field.WithEmbedded(_defintion.DefinitionType);
            field.WithReuseDefinition(_defintion);
        }
        if (_isGroup)
        {
            field.WithGroup(_propertyInfo.PropertyType.GenericTypeArguments[0]);
            field.WithConfigurations(new GroupMinMaxConfig(_min, _max));
        }
            
        return field;
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
    
    public IDefinitionFieldBuilder WithConfiguration(IMockatorConfiguration configuration)
    {
        _configurations.Add(configuration);
        return this;
    }

    public IDefinitionFieldBuilder AsGroup(int min, int max)
    {
        _min = min;
        _max = max;
        _isGroup = true;
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

    public IDefinitionBuilder HavingGroup(string propertyName, Definition groupDefinition)
    {
        var fieldBuilder = new DefinitionFieldBuilder(typeof(T), propertyName);
        fieldBuilder.UseDefinition(groupDefinition);
        fieldBuilder.AsGroup(1, 1);
        _definition.AddField(fieldBuilder.Build());
        
        return this;
    }

    public IDefinitionBuilder HavingGroup(string propertyName, Definition groupDefinition, int min, int max)
    {
        var fieldBuilder = new DefinitionFieldBuilder(typeof(T), propertyName);
        fieldBuilder.UseDefinition(groupDefinition);
        fieldBuilder.AsGroup(min, max);
        _definition.AddField(fieldBuilder.Build());
        
        return this;
    }
}

