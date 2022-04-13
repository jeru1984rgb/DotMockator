namespace dotmockator.core.generator;

public interface IRandomFieldResolver
{
    public IEnumerable<object> ResolveValue();
}