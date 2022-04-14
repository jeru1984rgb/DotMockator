using System.Collections.Generic;
using dotmockator.core.definitions;
using dotmockator.core.definitions.attributes;
using dotmockator.core.test.testdata.address;
using dotmockator.core.test.testdata.person;

namespace dotmockator.core.test.testdata.complex;

public class ComplexMockDefinitionWithAttribute : IComplexMockDefinition
{
    [MockatorEmbedded]
    [MockatorImplementationType(typeof(PersonMockDefinitionWithAttribute))]
    public IPersonMockDefinition? Person { get; set; }

    [MockatorGroup(1, 5)]
    [MockatorImplementationType(typeof(AddressMockDefinitionWithAttribute))]
    public List<IAddressMockDefinition>? Addresses { get; set; }
}