using DotMockator.Core.Definitions;
using DotMockator.Core.Definitions.Builder;
using DotMockator.Core.Generator.Address;

namespace DotMockator.Core.Test.TestData.Address;

public class AddressMockDefinitionWithBuilder : IAddressMockDefinition
{
    public string? City { get; set; }
    public string? Street { get; set; }
    
    public static Definition GetBuilder()
    {
        return new DefinitionBuilder<AddressMockDefinitionWithBuilder>()
            .HavingField(nameof(City), builder => builder.WithGenerator(typeof(AddressGenerator.CityName)))
            .HavingField(nameof(Street), builder => builder.WithGenerator(typeof(AddressGenerator.StreetName)))
            .Build();
    }
}