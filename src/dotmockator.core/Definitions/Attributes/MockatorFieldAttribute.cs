namespace DotMockator.Core.Definitions.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MockatorFieldAttribute : Attribute
{
    public Type GeneratorType { get; }

    public MockatorFieldAttribute(Type generatorType)
    {
        GeneratorType = generatorType;
    }
}