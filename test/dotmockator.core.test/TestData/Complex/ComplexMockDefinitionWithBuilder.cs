using System.Collections.Generic;
using DotMockator.Core.Definitions;
using DotMockator.Core.Definitions.Builder;
using DotMockator.Core.Test.TestData.Address;
using DotMockator.Core.Test.TestData.Person;

namespace DotMockator.Core.Test.TestData.Complex;

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