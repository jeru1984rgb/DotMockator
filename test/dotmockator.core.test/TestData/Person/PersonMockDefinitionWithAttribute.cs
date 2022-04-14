using System;
using DotMockator.Core.Definitions.Attributes;
using DotMockator.Core.Generator.Date;
using DotMockator.Core.Generator.Lorem;
using DotMockator.Core.Generator.Name;

namespace DotMockator.Core.Test.TestData.Person;

public class PersonMockDefinitionWithAttribute : IPersonMockDefinition
{
    [MockatorField(typeof(NameGenerator.FirstName))]
    public string? FirstName { get; set; }

    [MockatorField(typeof(NameGenerator.LastName))]
    public string? LastName { get; set; }

    [MockatorField(typeof(DateGenerator.Past))]
    [DateGenerator.PastConfig(50)]
    public DateTime? BirthDate { get; set; }

    [MockatorField(typeof(LoremGenerator.Sentence))]
    public string? Slogan { get; set; }

    [MockatorField(typeof(LoremGenerator.Paragraph))]
    public string? PersonalDescription { get; set; }

    [DynamicMockatorFieldResolver(typeof(GuidResolver))]
    public Guid? DynamicResolvedGuid { get; set; }

    [DynamicMockatorFieldResolver(typeof(StringResolver))]
    public string? DynamicResolvedString { get; set; }
    
}