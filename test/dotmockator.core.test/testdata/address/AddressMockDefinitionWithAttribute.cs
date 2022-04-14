using dotmockator.core.definitions;
using dotmockator.core.generator.address;

namespace dotmockator.core.test.testdata.address;

public class AddressMockDefinitionWithAttribute : IAddressMockDefinition
{
    [MockatorField(typeof(AddressGenerator.CityName))]
    public string? City { get; set; }

    [MockatorField(typeof(AddressGenerator.StreetName))]
    public string? Street { get; set; }
    
}