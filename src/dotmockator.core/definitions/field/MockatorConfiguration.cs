namespace dotmockator.core.definitions.field;

public abstract class MockatorConfiguration : IMockatorConfiguration
{
    public MockatorConfiguration()
    {
    }

    public abstract void DefaultConfiguration();
}