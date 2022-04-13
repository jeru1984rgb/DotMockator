namespace dotmockator.core.definitions;

public class DynamicMockatorFieldResolverAttribute : Attribute
{
    public Type ResolverType { get; }

    public DynamicMockatorFieldResolverAttribute(Type resolverType)
    {
        ResolverType = resolverType;
    }
}