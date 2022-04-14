using System.Collections.Generic;
using dotmockator.core.definitions;
using dotmockator.core.definitions.field;
using dotmockator.core.generator.strategies;
using dotmockator.core.test.testdata.complex;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.generator.strategies;

public class GroupStrategyTest
{
    public static IEnumerable<object[]> GetComplexTestSet()
        => new[]
        {
            /*new object[]
            {
                DefinitionExtractor.ExtractDefinition(typeof(ComplexMockDefinitionWithAttribute)),
                new ComplexMockDefinitionWithAttribute()
            },
            */
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
        candidate.Addresses.Should().BeNull();
        var df = definition.GetFieldByPropertyName(nameof(candidate.Addresses));
        df.Should().NotBeNull();
        var config = df!.GetConfiguration<GroupMinMaxConfig>();
        config.Min.Should().Be(1);
        config.Max.Should().Be(5);
        GroupStrategy.HandleGroup(candidate, df!);
        candidate.Addresses.Should().NotBeNull().And.NotBeEmpty();
    }
}