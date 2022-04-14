namespace dotmockator.core.definitions.field;

public abstract class MockatorAttributeConfiguration : Attribute, IMockatorConfiguration
{
    public MockatorAttributeConfiguration()
    {
    }

    public abstract void DefaultConfiguration();
}