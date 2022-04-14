namespace DotMockator.Core.Definitions.Field;

public class GroupMinMaxConfig : MockatorConfiguration
{
    public int Min { get; private set; }
    public int Max { get; private set; }
    
    public GroupMinMaxConfig(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public override void DefaultConfiguration()
    {
        Min = 1;
        Max = 1;
    }
}