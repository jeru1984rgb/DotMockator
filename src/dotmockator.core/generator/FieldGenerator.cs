using dotmockator.core.definitions.field;

namespace dotmockator.core.generator;

public class FieldGenerator : IGenerator
{
    private readonly Func<DefinitionField, object> _generator;

    public FieldGenerator(Func<DefinitionField, object> generator)
    {
        _generator = generator;
    }

    public object Generate()
    {
        return _generator.Invoke(DefinitionField.EmptyDefinitionField);
    }
    
    public object Generate(DefinitionField df)
    {
        return _generator.Invoke(df);
    }
}