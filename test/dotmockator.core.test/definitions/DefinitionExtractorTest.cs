using System.Linq;
using dotmockator.core.definitions;
using dotmockator.core.definitions.field;
using dotmockator.core.generator.date;
using dotmockator.core.generator.lorem;
using dotmockator.core.generator.name;
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
        firstNameField.GeneratorType.Value.Should().Be(typeof(NameGenerator.FirstName));

        var lastNameField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.LastName));
        lastNameField.GeneratorType.Value.Should().Be(typeof(NameGenerator.LastName));
        
        var birthDateField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.BirthDate));
        birthDateField.GeneratorType.Value.Should().Be(typeof(DateGenerator.Past));
        
        var sloganField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.Slogan));
        sloganField.GeneratorType.Value.Should().Be(typeof(LoremGenerator.Sentence));
        
        var personalDescriptionField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.PersonalDescription));
        personalDescriptionField.GeneratorType.Value.Should().Be(typeof(LoremGenerator.Paragraph));
        
        var dynamicResolvedGuidField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.DynamicResolvedGuid));
        dynamicResolvedGuidField.ResolverType.Value.Should().Be(typeof(GuidResolver));

        var dynamicResolvedStringField =
            definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.DynamicResolvedString));
        dynamicResolvedStringField.ResolverType.Value.Should().Be(typeof(StringResolver));
    }

    [Fact]
    public void DefinitionExtractor_ComplexDefinition_GetImplementationType()
    {
        Definition definition = DefinitionExtractor.ExtractDefinition(typeof(ComplexMockDefinitionWithAttribute));
        definition.Fields.Count().Should().Be(2);
        var addressField = definition.Fields.First(x => x.PropertyName == nameof(ComplexMockDefinitionWithAttribute.Addresses));
        addressField.GroupType.IsPresent.Should().BeTrue();
        addressField.GroupType.Value.Should().Be(typeof(IAddressMockDefinition));

        addressField.ImplementationType.IsPresent.Should().BeTrue();
        addressField.ImplementationType.Value.Should().Be(typeof(AddressMockDefinitionWithAttribute));

        addressField.GeneratorType.IsPresent.Should().BeFalse();

        var minMaxConfig = addressField.GetConfiguration<GroupMinMaxConfig>();
        
        minMaxConfig.Min.Should().Be(1);
        minMaxConfig.Max.Should().Be(5);

        var personField = definition.Fields.First(x => x.PropertyName == nameof(ComplexMockDefinitionWithAttribute.Person));
        personField.EmbeddedType.IsPresent.Should().BeTrue();
    }

    [Fact]
    public void DefinitionExtractor_GeneratorConfigAttribute_GetsExtracted()
    {
        Definition definition = DefinitionExtractor.ExtractDefinition(typeof(PersonMockDefinitionWithAttribute));
        var birthDateField = definition.Fields.First(x => x.PropertyName == nameof(PersonMockDefinitionWithAttribute.BirthDate));
        var config = birthDateField.GetConfiguration<DateGenerator.PastConfig>();
        config.YearsToGoBack.Should().Be(50);
    }
}