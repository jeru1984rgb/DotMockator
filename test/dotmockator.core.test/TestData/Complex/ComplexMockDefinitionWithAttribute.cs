using System.Collections.Generic;
using DotMockator.Core.Definitions.Attributes;
using DotMockator.Core.Test.TestData.Address;
using DotMockator.Core.Test.TestData.Person;

namespace DotMockator.Core.Test.TestData.Complex;

public class ComplexMockDefinitionWithAttribute : IComplexMockDefinition
{
    [MockatorEmbedded]
    [MockatorImplementationType(typeof(PersonMockDefinitionWithAttribute))]
    public IPersonMockDefinition? Person { get; set; }

    [MockatorGroup(1, 5)]
    [MockatorImplementationType(typeof(AddressMockDefinitionWithAttribute))]
    public List<IAddressMockDefinition>? Addresses { get; set; }
}