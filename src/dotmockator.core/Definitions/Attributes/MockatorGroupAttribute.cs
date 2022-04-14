namespace DotMockator.Core.Definitions.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MockatorGroupAttribute : Attribute
{
    public int Min { get; }
    public int Max { get; }
    
    public MockatorGroupAttribute(int min, int max)
    {
        Min = min;
        Max = max;
    }

}