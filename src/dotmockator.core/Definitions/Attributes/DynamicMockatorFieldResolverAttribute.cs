namespace DotMockator.Core.Definitions.Attributes;

public class DynamicMockatorFieldResolverAttribute : Attribute
{
    public Type ResolverType { get; }

    public DynamicMockatorFieldResolverAttribute(Type resolverType)
    {
        ResolverType = resolverType;
    }
}