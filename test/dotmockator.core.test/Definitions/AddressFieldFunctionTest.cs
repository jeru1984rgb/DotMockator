using DotMockator.Core.Generator.Address;
using FluentAssertions;
using Xunit;

namespace DotMockator.Core.Test.Definitions;

public class AddressFieldFunctionTest
{
    [Fact]
    public void AddressCityNameFunction_Test()
    {
        var result = (string) new AddressGenerator.CityName().Generate();
        result.Should().NotBeEmpty();
    }
}