using dotmockator.core.definitions.field;

namespace dotmockator.core.generator;

public interface IGenerator
{
    public object Generate(DefinitionField df);
}