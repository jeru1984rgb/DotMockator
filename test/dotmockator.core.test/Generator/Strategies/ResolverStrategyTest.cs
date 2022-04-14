using System;
using System.Collections.Generic;
using DotMockator.Core.Definitions;
using DotMockator.Core.Generator.Strategies;
using DotMockator.Core.Test.TestData.Person;
using FluentAssertions;
using Xunit;

namespace DotMockator.Core.Test.Generator.Strategies;

public class ResolverStrategyTest
{
    public static IEnumerable<object[]> GetPersonTestSet()
        => new[]
        {
            new object[]
            {
                DefinitionExtractor.ExtractDefinition(typeof(PersonMockDefinitionWithAttribute)),
                new PersonMockDefinitionWithAttribute()
            },
            new object[]
            {
                PersonMockDefinitionWithBuilder.GetBuilder(),
                new PersonMockDefinitionWithBuilder()
            }
        };

    [Theory]
    [MemberData(nameof(GetPersonTestSet))]
    public void HandleField_PopulatesField(Definition definition, IPersonMockDefinition candidate)
    {
        candidate.DynamicResolvedGuid.Should().BeNull();
        var df = definition.GetFieldByPropertyName(nameof(candidate.DynamicResolvedGuid));
        df.Should().NotBeNull();
        ResolverStrategy.HandleResolver(candidate, df!);
        candidate.DynamicResolvedGuid.Should().NotBe(Guid.Empty);
    }
}