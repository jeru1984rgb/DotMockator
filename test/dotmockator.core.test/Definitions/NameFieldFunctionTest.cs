using DotMockator.Core.Generator.Name;
using FluentAssertions;
using Xunit;

namespace DotMockator.Core.Test.Definitions;

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