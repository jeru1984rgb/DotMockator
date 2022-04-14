namespace DotMockator.Core.Definitions.Attributes;

public class MockatorImplementationTypeAttribute : Attribute
{
    public Type ImplementationType { get; private set; }
    public MockatorImplementationTypeAttribute(Type type)
    {
        ImplementationType = type;
    }
}