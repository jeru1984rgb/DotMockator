using dotmockator.core.definitions;
using dotmockator.core.generator;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.definitions;

public class AddressFieldFunctionTest
{
    [Fact]
    public void AddressCityNameFunction_Test()
    {
        var result = (string) AddressFieldGenerator.AddressCityNameGenerator.Generate();
        result.Should().NotBeEmpty();
    }
}