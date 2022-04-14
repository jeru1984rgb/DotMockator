using System.Collections.Generic;
using DotMockator.Core.Definitions;
using DotMockator.Core.Generator.Strategies;
using DotMockator.Core.Test.TestData.Complex;
using FluentAssertions;
using Xunit;

namespace DotMockator.Core.Test.Generator.Strategies;

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