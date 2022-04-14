using System.Collections.Generic;
using DotMockator.Core.Test.TestData.Address;
using DotMockator.Core.Test.TestData.Person;

namespace DotMockator.Core.Test.TestData.Complex;

public interface IComplexMockDefinition
{
    public IPersonMockDefinition? Person { get; set; }
    
    public List<IAddressMockDefinition>? Addresses { get; set; }
}