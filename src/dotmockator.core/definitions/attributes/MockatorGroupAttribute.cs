namespace dotmockator.core.definitions;

[AttributeUsage(AttributeTargets.Property)]
public class MockatorGroupAttribute : Attribute
{
    private int Min { get; }
    private int Max { get; }
    
    public MockatorGroupAttribute(int max, int min)
    {
        Max = max;
        Min = min;
    }

}