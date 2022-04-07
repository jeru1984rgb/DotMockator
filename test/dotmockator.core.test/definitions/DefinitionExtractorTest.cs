using System.Linq;
using dotmockator.core.definitions;
using FluentAssertions;
using Xunit;

namespace dotmockator.core.test.definitions;

public class DefinitionExtractorTest
{
    [Fact]
    public void DefinitionExtractor_SimpleDefinition_GotAllDefinitions()
    {
        Definition definition = DefinitionExtractor.ExtractDefinition<SimpleMockDefinition>();
        definition.Fields.Count().Should().Be(2);
        definition.Fields.ElementAt(0).PropertyName.Should().Be("FirstName");
        definition.Fields.ElementAt(0).FieldFunction.Should().Be(FieldFunctionEnum.FirstName);
        definition.Fields.ElementAt(1).PropertyName.Should().Be("LastName");
        definition.Fields.ElementAt(1).FieldFunction.Should().Be(FieldFunctionEnum.LastName);
    }
}




