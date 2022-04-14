using System.Collections.Generic;
using dotmockator.core.definitions;
using dotmockator.core.generator.strategies;
using dotmockator.core.test.testdata.complex;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.generator.strategies;

public class EmbeddedStrategyTest
{
    public static IEnumerable<object[]> GetComplexTestSet()
        => new[]
        {
            new object[]
            {
                DefinitionExtractor.ExtractDefinition(typeof(ComplexMockDefinitionWithAttribute)),
                new ComplexMockDefinitionWithAttribute()
            },
            new object[]
            {
                ComplexMockDefinitionWithBuilder.GetBuilder(),
                new ComplexMockDefinitionWithBuilder()
            }
        };
    
    [Theory]
    [MemberData(nameof(GetComplexTestSet))]
    public void HandleEmbedded_PopulatesField(Definition definition, IComplexMockDefinition candidate)
    {
        candidate.Person.Should().BeNull();
        var df = definition.GetFieldByPropertyName(nameof(candidate.Person));
        df.Should().NotBeNull();
        EmbeddedStrategy.HandleEmbedded(candidate, df!);
        candidate.Person.Should().NotBeNull();
    }
}