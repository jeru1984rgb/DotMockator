namespace DotMockator.Core.Definitions.Field;

public abstract class MockatorAttributeConfiguration : Attribute, IMockatorConfiguration
{
    public abstract void DefaultConfiguration();
}