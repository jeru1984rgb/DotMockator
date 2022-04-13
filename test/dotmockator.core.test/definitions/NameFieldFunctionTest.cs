using dotmockator.core.definitions;
using dotmockator.core.generator;
using dotmockator.core.generator.name;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.definitions;

public class NameFieldFunctionTest
{
    [Fact]
    public void FirstNameFunction_Test()
    {
        var result = (string) new NameGenerator.FirstName().Generate();
        result.Should().NotBeEmpty();
    }
    [Fact]
    public void LastNameFunction_Test()
    {
        var result = (string) new NameGenerator.LastName().Generate();
        result.Should().NotBeEmpty();
    }
}