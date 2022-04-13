using dotmockator.core.definitions;

namespace dotmockator.core.generator;

public interface IGenerator
{
    public object Generate(DefinitionField df);
}