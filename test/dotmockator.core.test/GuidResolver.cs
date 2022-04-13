using System;
using dotmockator.core.generator;

namespace dotmockator.core.test;

public class GuidResolver : IDynamicFieldResolver
{
    public static Guid TestGuid = Guid.Parse("c778f480-9039-4923-bcbf-5271d5996094"); 
    
    public object ResolveValue()
    {
        return TestGuid;
    }
}