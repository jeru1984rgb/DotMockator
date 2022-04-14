using System.Collections.Generic;
using dotmockator.core.definitions;
using dotmockator.core.definitions.builder;
using dotmockator.core.test.testdata.address;
using dotmockator.core.test.testdata.person;

namespace dotmockator.core.test.testdata.complex;

public class ComplexMockDefinitionWithBuilder : IComplexMockDefinition
{
    public IPersonMockDefinition? Person { get; set; }
    public List<IAddressMockDefinition>? Addresses { get; set; }

    public static Definition GetBuilder()
    {
        return new DefinitionBuilder<ComplexMockDefinitionWithBuilder>()
            .HavingField(nameof(Person),
                builder => builder.UseDefinition(PersonMockDefinitionWithBuilder.GetBuilder()))
            .HavingGroup(nameof(Addresses),
                AddressMockDefinitionWithBuilder.GetBuilder(), 1, 5)
            .Build();
    }
}