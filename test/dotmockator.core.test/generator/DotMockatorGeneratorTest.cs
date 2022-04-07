using dotmockator.core.generator;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.generator;

public class DotMockatorGeneratorTest
{
    [Fact]
    public void GenerateSingle_ShouldCreateInstanceOfTargetType()
    {
        DotMockatorGenerator generator = new DotMockatorGenerator();
        var result = generator.GenerateSingle<SimpleMockDefinition>();
        result.Should().BeOfType<SimpleMockDefinition>();
    }

    [Fact]
    public void GenerateSingle_PropertiesShouldNotBeNull()
    {
        DotMockatorGenerator generator = new DotMockatorGenerator();
        var result = generator.GenerateSingle<SimpleMockDefinition>();
        result.Should().BeOfType<SimpleMockDefinition>();
        result.FirstName.Should().NotBeNull().And.NotBeEmpty();
        result.LastName.Should().NotBeNull().And.NotBeEmpty();
    }
}