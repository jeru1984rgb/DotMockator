using System.Collections.Generic;
using dotmockator.core.test.testdata.address;
using dotmockator.core.test.testdata.person;

namespace dotmockator.core.test.testdata.complex;

public interface IComplexMockDefinition
{
    public IPersonMockDefinition Person { get; set; }
    
    public List<IAddressMockDefinition> Addresses { get; set; }
}