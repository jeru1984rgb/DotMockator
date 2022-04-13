using System;
using System.Collections.Generic;
using System.Linq;
using dotmockator.core.generator;
using dotmockator.core.test.testdata.address;
using dotmockator.core.test.testdata.complex;
using dotmockator.core.test.testdata.person;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.generator;

public class DotMockatorGeneratorTest
{
    public static IEnumerable<object[]> GetPersonMocks()
        => new[]
        {
            new object[]
            {
                MockatorGenerator.GenerateSingle<PersonMockDefinitionWithBuilder>(PersonMockDefinitionWithBuilder
                    .GetBuilder())
            },
            new object[] {MockatorGenerator.GenerateSingle<PersonMockDefinitionWithAttribute>()}
        };

    public static IEnumerable<object[]> GetPersonMockObservable()
        => new[]
        {
            new[]
            {
                MockatorGenerator.GenerateObservable<PersonMockDefinitionWithBuilder>(10,
                    PersonMockDefinitionWithBuilder.GetBuilder())
            },
            new object[] {MockatorGenerator.GenerateObservable<PersonMockDefinitionWithAttribute>(10)}
        };

    public static IEnumerable<object[]> GetAddressMocks()
        => new[]
        {
            new object[]
            {
                MockatorGenerator.GenerateSingle<AddressMockDefinitionWithBuilder>(AddressMockDefinitionWithBuilder
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
            new[]
            {
                MockatorGenerator.GenerateObservable<AddressMockDefinitionWithBuilder>(10,
                    AddressMockDefinitionWithBuilder.GetBuilder())
            }
        };

    public static IEnumerable<object[]> GetComplexMocks()
        => new[]
        {
            new object[]
            {
                MockatorGenerator.GenerateSingle<ComplexMockDefinitionWithBuilder>(ComplexMockDefinitionWithBuilder
                    .GetBuilder())
            },
            new object[]
            {
                MockatorGenerator.GenerateSingle<ComplexMockDefinitionWithAttribute>()
            }
        };

    void AssertPersonMock(IPersonMockDefinition personMockDefinitionWithAttributes)
    {
        personMockDefinitionWithAttributes.FirstName.Should().NotBeNull().And.NotBeEmpty();
        personMockDefinitionWithAttributes.LastName.Should().NotBeNull().And.NotBeEmpty();
        personMockDefinitionWithAttributes.Slogan.Should().NotBeNull().And.NotBeEmpty();
        personMockDefinitionWithAttributes.PersonalDescription.Should().NotBeNull().And.NotBeEmpty();
        personMockDefinitionWithAttributes.DynamicResolvedGuid.Should().Be(GuidResolver.TestGuid);
    }

    private void AssertAddressMock(IAddressMockDefinition addressMockDefinitionWithAttribute)
    {
        addressMockDefinitionWithAttribute.City.Should().NotBeNull().And.NotBeEmpty();
        addressMockDefinitionWithAttribute.Street.Should().NotBeNull().And.NotBeEmpty();
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
        AssertPersonMock(candidate.Person);
        candidate.Addresses.Should().NotBeNull().And.NotBeEmpty();
        candidate.Addresses.Count().Should().BeGreaterThan(0);
        candidate.Addresses.ForEach(AssertAddressMock);
    }
}