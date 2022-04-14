using System.Collections.Generic;
using dotmockator.core.definitions;
using dotmockator.core.generator.strategies;
using dotmockator.core.test.testdata.address;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.generator.strategies;

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