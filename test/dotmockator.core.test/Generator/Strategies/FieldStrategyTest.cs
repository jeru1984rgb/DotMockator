using System.Collections.Generic;
using DotMockator.Core.Definitions;
using DotMockator.Core.Generator.Strategies;
using DotMockator.Core.Test.TestData.Address;
using FluentAssertions;
using Xunit;

namespace DotMockator.Core.Test.Generator.Strategies;

public class FieldStrategyTest
{
    public static IEnumerable<object[]> GetAddressTestSet()
        => new[]
        {
            new object[]
            {
                DefinitionExtractor.ExtractDefinition(typeof(AddressMockDefinitionWithAttribute)),
                new AddressMockDefinitionWithAttribute()
            },
            new object[]
            {
                AddressMockDefinitionWithBuilder.GetBuilder(),
                new AddressMockDefinitionWithBuilder()
            }
        };

    [Theory]
    [MemberData(nameof(GetAddressTestSet))]
    public void HandleField_PopulatesField(Definition definition, IAddressMockDefinition candidate)
    {
        candidate.Street.Should().BeNull();
        var df = definition.GetFieldByPropertyName(nameof(candidate.Street));
        df.Should().NotBeNull();
        FieldStrategy.HandleField(candidate, df!);
        candidate.Street.Should().NotBeEmpty();

        candidate.City.Should().BeNull();
        df = definition.GetFieldByPropertyName(nameof(candidate.City));
        df.Should().NotBeNull();
        FieldStrategy.HandleField(candidate, df!);
        candidate.City.Should().NotBeEmpty();
    }
}