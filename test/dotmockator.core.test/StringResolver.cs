using DotMockator.Core.Generator;

namespace DotMockator.Core.Test;

public class StringResolver : IDynamicFieldResolver
{
    public static string TestString = "FooBar";
    public object ResolveValue()
    {
        return TestString;
    }
}