namespace DotMockator.Core.Generator;

public interface IRandomFieldResolver
{
    public IEnumerable<object> ResolveValue();
}