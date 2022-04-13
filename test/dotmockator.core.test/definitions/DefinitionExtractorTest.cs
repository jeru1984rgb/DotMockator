using System.Linq;
using dotmockator.core.definitions;
using dotmockator.core.generator.date;
using dotmockator.core.generator.lorem;
using dotmockator.core.generator.name;
using dotmockator.core.test.testdata;
using dotmockator.core.test.testdata.address;
using dotmockator.core.test.testdata.complex;
using dotmockator.core.test.testdata.person;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.definitions;

public class DefinitionExtractorTest
{
    [Fact]
    public void DefinitionExtractor_SimpleDefinition_GotAllDefinitions()
    {
        Definition definition = DefinitionExtractor.ExtractDefinition(typeof(PersonMockDefinitionWithAttribute));
        definition.Fields.Count().Should().Be(7);
        
        var firstNameField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.FirstName));
        firstNameField.GeneratorType.Should().Be(typeof(NameGenerator.FirstName));

        var lastNameField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.LastName));
        lastNameField.GeneratorType.Should().Be(typeof(NameGenerator.LastName));
        
        var birthDateField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.BirthDate));
        birthDateField.GeneratorType.Should().Be(typeof(DateGenerator.Past));
        
        var sloganField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.Slogan));
        sloganField.GeneratorType.Should().Be(typeof(LoremGenerator.Sentence));
        
        var personalDescriptionField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.PersonalDescription));
        personalDescriptionField.GeneratorType.Should().Be(typeof(LoremGenerator.Paragraph));
        
        var dynamicResolvedGuidField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.DynamicResolvedGuid));
        dynamicResolvedGuidField.ResolverType.Should().Be(typeof(GuidResolver));

        var dynamicResolvedStringField =
            definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.DynamicResolvedString));
        dynamicResolvedStringField.ResolverType.Should().Be(typeof(StringResolver));
    }

    [Fact]
    public void DefinitionExtractor_ComplexDefinition_GetGroupType()
    {
        Definition definition = DefinitionExtractor.ExtractDefinition(typeof(ComplexMockDefinitionWithAttribute));
        definition.Fields.Count().Should().Be(2);
        var addressField = definition.Fields.First(x => x.PropertyName == nameof(ComplexMockDefinitionWithAttribute.Addresses));
        addressField.GroupType.Should().Be(typeof(IAddressMockDefinition));
        addressField.ImplementationType.Should().Be(typeof(AddressMockDefinitionWithAttribute));
        addressField.IsGroup.Should().BeTrue();
        addressField.IsGenerator.Should().BeFalse();
        addressField.Min.Should().Be(1);
        addressField.Max.Should().Be(5);

        var personField = definition.Fields.First(x => x.PropertyName == nameof(ComplexMockDefinitionWithAttribute.Person));
        personField.IsEmbedded.Should().BeTrue();
    }

    [Fact]
    public void DefinitionExtractor_GeneratorConfigAttribute_GetsExtracted()
    {
        Definition definition = DefinitionExtractor.ExtractDefinition(typeof(PersonMockDefinitionWithAttribute));
        var birthDateField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.BirthDate));
        var config = birthDateField.GetGeneratorConfig<DateGenerator.PastConfig>();
        config.Should().NotBeNull();
        config!.YearsToGoBack.Should().Be(50);
    }
}