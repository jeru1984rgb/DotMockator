using System;
using dotmockator.core.definitions;
using dotmockator.core.generator.date;
using dotmockator.core.generator.lorem;
using dotmockator.core.generator.name;

namespace dotmockator.core.test.testdata.person;

public class PersonMockDefinitionWithAttribute : IPersonMockDefinition
{
    [MockatorField(typeof(NameGenerator.FirstName))]
    public string FirstName { get; set; }

    [MockatorField(typeof(NameGenerator.LastName))]
    public string LastName { get; set; }

    [MockatorField(typeof(DateGenerator.Past))]
    [DateGenerator.PastConfig(50)]
    public DateTime BirthDate { get; set; }

    [MockatorField(typeof(LoremGenerator.Sentence))]
    public string Slogan { get; set; }

    [MockatorField(typeof(LoremGenerator.Paragraph))]
    public string PersonalDescription { get; set; }

    [DynamicMockatorFieldResolver(typeof(GuidResolver))]
    public Guid DynamicResolvedGuid { get; set; }

    [DynamicMockatorFieldResolver(typeof(StringResolver))]
    public string DynamicResolvedString { get; set; }
    
}