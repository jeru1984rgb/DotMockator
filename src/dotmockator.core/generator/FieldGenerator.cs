using dotmockator.core.definitions;

namespace dotmockator.core.generator;

public class FieldGenerator : IGenerator
{
    private readonly Func<object> _generator;
    public FieldFunctionEnum FunctionEnum { get; }

    public FieldGenerator(Func<object> generator, FieldFunctionEnum fieldFunctionEnum)
    {
        _generator = generator;
        FunctionEnum = fieldFunctionEnum;
    }

    public object Generate()
    {
        return _generator.Invoke();
    }
}