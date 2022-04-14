using DotMockator.Core.Definitions.Field;

namespace DotMockator.Core.Generator;

public interface IGenerator
{
    public object Generate(DefinitionField df);
}