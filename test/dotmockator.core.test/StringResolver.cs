using dotmockator.core.generator;

namespace dotmockator.core.test;

public class StringResolver : IDynamicFieldResolver
{
    public static string TestString = "FooBar";
    public object ResolveValue()
    {
        return TestString;
    }
}