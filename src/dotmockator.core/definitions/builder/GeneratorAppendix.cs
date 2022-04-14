using dotmockator.core.definitions.builder;

namespace dotmockator.core.definitions;

public class GeneratorAppendix : IBuilderAppendix
{
    public Type GeneratorType { get; }

    public GeneratorAppendix(Type generatorType)
    {
        GeneratorType = generatorType;
    }
    
    public static IBuilderAppendix WithGenerator(Type generatorType)
    {
        return new GeneratorAppendix(generatorType);
    }
}