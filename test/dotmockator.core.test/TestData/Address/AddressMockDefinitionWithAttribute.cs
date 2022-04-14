using DotMockator.Core.Definitions.Attributes;
using DotMockator.Core.Generator.Address;

namespace DotMockator.Core.Test.TestData.Address;

public class AddressMockDefinitionWithAttribute : IAddressMockDefinition
{
    [MockatorField(typeof(AddressGenerator.CityName))]
    public string? City { get; set; }

    [MockatorField(typeof(AddressGenerator.StreetName))]
    public string? Street { get; set; }
    
}