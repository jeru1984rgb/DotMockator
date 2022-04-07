using dotmockator.core.definitions;

namespace dotmockator.core.generator;

public interface IGenerator
{
    public FieldFunctionEnum FunctionEnum { get; }

    public object Generate();
}