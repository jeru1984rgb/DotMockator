using dotmockator.core.generator;
using dotmockator.core.generator.address;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.definitions;

public class AddressFieldFunctionTest
{
    [Fact]
    public void AddressCityNameFunction_Test()
    {
        var result = (string) new AddressGenerator.CityName().Generate();
        result.Should().NotBeEmpty();
    }
}