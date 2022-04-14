using System;
using System.Collections.Generic;
using DotMockator.Core.Generator;

namespace DotMockator.Core.Test;

public class GuidsResolver : IRandomFieldResolver
{
    public static IEnumerable<Guid> TestGuids = new[]
    {
        Guid.Parse("941d31f3-092e-4a20-9800-c398abb16217"),
        Guid.Parse("235adcd5-1643-443f-8e75-fd7303d2e34d"),
        Guid.Parse("6d473ed8-de01-4bbe-976e-b2d1b2ada366"),
        Guid.Parse("6a005e32-b56d-4919-be25-d5c1bd0bef30")
    };

    public IEnumerable<object> ResolveValue()
    {
        return (IEnumerable<object>) TestGuids;
    }
}