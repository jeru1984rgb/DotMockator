using dotmockator.core.definitions;
using dotmockator.core.definitions.builder;
using dotmockator.core.generator.address;

namespace dotmockator.core.test.testdata.address;

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