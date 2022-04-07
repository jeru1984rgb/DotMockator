using dotmockator.core.definitions;
using dotmockator.core.generator;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.definitions;

public class NameFieldFunctionTest
{
    [Fact]
    public void FirstNameFunction_Test()
    {
        var result = (string) NameFieldGenerator.FirstNameGenerator.Generate();
        result.Should().NotBeEmpty();
    }
    [Fact]
    public void LastNameFunction_Test()
    {
        var result = (string) NameFieldGenerator.LastNameGenerator.Generate();
        result.Should().NotBeEmpty();
    }
}