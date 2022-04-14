using System.Collections.Generic;
using DotMockator.Core.Definitions;
using DotMockator.Core.Definitions.Field;
using DotMockator.Core.Generator.Strategies;
using DotMockator.Core.Test.TestData.Complex;
using FluentAssertions;
using Xunit;

namespace DotMockator.Core.Test.Generator.Strategies;

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
        GroupStrategy.HandleGroup(candidate, df);
        candidate.Addresses.Should().NotBeNull().And.NotBeEmpty();
    }
}