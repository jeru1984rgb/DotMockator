using System;
using System.Collections.Generic;
using System.Linq;
using DotMockator.Core.Generator;
using DotMockator.Core.Test.TestData.Address;
using DotMockator.Core.Test.TestData.Complex;
using DotMockator.Core.Test.TestData.Person;
using FluentAssertions;
using Xunit;

namespace DotMockator.Core.Test.Generator;

public class DotMockatorGeneratorTest
{
    public static IEnumerable<object[]> GetPersonMocks()
        => new[]
        {
            new []
            {
                MockatorGenerator.GenerateSingle(PersonMockDefinitionWithBuilder
                    .GetBuilder())
            },
            new object[] {MockatorGenerator.GenerateSingle<PersonMockDefinitionWithAttribute>()}
        };

    public static IEnumerable<object[]> GetPersonMockObservable()
        => new[]
        {
            new object[]
            {
                MockatorGenerator.GenerateObservable<PersonMockDefinitionWithBuilder>(10,
                    PersonMockDefinitionWithBuilder.GetBuilder())
            },
            new object[] {MockatorGenerator.GenerateObservable<PersonMockDefinitionWithAttribute>(10)}
        };

    public static IEnumerable<object[]> GetAddressMocks()
        => new[]
        {
            new []
            {
                MockatorGenerator.GenerateSingle(AddressMockDefinitionWithBuilder
                    .GetBuilder())
            },
            new object[] {MockatorGenerator.GenerateSingle<AddressMockDefinitionWithAttribute>()}
        };

    public static IEnumerable<object[]> GetAddressMockObservable()
        => new[]
        {
            new object[]
            {
                MockatorGenerator.GenerateObservable<AddressMockDefinitionWithAttribute>(10)
            },
            new object[]
            {
                MockatorGenerator.GenerateObservable<AddressMockDefinitionWithBuilder>(10,
                    AddressMockDefinitionWithBuilder.GetBuilder())
            }
        };

    public static IEnumerable<object[]> GetComplexMocks()
        => new[]
        {
            new []
            {
                MockatorGenerator.GenerateSingle(ComplexMockDefinitionWithBuilder
                    .GetBuilder())
            },
            new object[]
            {
                MockatorGenerator.GenerateSingle<ComplexMockDefinitionWithAttribute>()
            }
        };

    void AssertPersonMock(IPersonMockDefinition personMock)
    {
        personMock.FirstName.Should().NotBeNull().And.NotBeEmpty();
        personMock.LastName.Should().NotBeNull().And.NotBeEmpty();
        personMock.Slogan.Should().NotBeNull().And.NotBeEmpty();
        personMock.PersonalDescription.Should().NotBeNull().And.NotBeEmpty();
        personMock.DynamicResolvedGuid.Should().Be(GuidResolver.TestGuid);
    }

    private void AssertAddressMock(IAddressMockDefinition addressMock)
    {
        addressMock.City.Should().NotBeNull().And.NotBeEmpty();
        addressMock.Street.Should().NotBeNull().And.NotBeEmpty();
    }


    [Theory]
    [MemberData(nameof(GetPersonMocks))]
    public void GenerateSingle_PersonMock_ShouldCreateInstanceOfTargetType(IPersonMockDefinition candidate)
    {
        candidate.Should().BeAssignableTo<IPersonMockDefinition>();
    }

    [Theory]
    [MemberData(nameof(GetAddressMocks))]
    public void GenerateSingle_AddressMock_ShouldCreateInstanceOfTargetType(IAddressMockDefinition candidate)
    {
        candidate.Should().BeAssignableTo<IAddressMockDefinition>();
    }


    [Theory]
    [MemberData(nameof(GetPersonMocks))]
    public void GenerateSingle_PersonMock_PropertiesShouldNotBeNull(IPersonMockDefinition candidate)
    {
        AssertPersonMock(candidate);
    }

    [Theory]
    [MemberData(nameof(GetAddressMocks))]
    public void GenerateSingle_AddressMock_PropertiesShouldNotBeNull(IAddressMockDefinition candidate)
    {
        AssertAddressMock(candidate);
    }

    [Theory]
    [MemberData(nameof(GetPersonMockObservable))]
    public void GenerateObserver_PersonMock_ObserverGetsCreated(IObservable<IPersonMockDefinition> observable)
    {
        observable.Subscribe(AssertPersonMock);
    }

    [Theory]
    [MemberData(nameof(GetAddressMockObservable))]
    public void GenerateObserver_AddressMock_ObserverGetsCreated(IObservable<IAddressMockDefinition> observable)
    {
        observable.Subscribe(AssertAddressMock);
    }

    [Theory]
    [MemberData(nameof(GetComplexMocks))]
    public void GenerateSingle_ComplexMockDefinition(IComplexMockDefinition candidate)
    {
        AssertPersonMock(candidate.Person!);
        candidate.Addresses.Should().NotBeNull().And.NotBeEmpty();
        candidate.Addresses!.Count().Should().BeGreaterThan(0);
        candidate.Addresses!.ForEach(AssertAddressMock);
    }
}