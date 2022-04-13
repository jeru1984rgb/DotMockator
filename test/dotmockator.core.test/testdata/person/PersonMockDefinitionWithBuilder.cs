using System;
using dotmockator.core.definitions;
using dotmockator.core.definitions.builder;
using dotmockator.core.generator.date;
using dotmockator.core.generator.lorem;
using dotmockator.core.generator.name;

namespace dotmockator.core.test.testdata.person;

public class PersonMockDefinitionWithBuilder : IPersonMockDefinition
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Slogan { get; set; }
    public string PersonalDescription { get; set; }
    public Guid DynamicResolvedGuid { get; set; }
    public string DynamicResolvedString { get; set; }

    public static Definition GetBuilder()
    {
        return new DefinitionBuilder<PersonMockDefinitionWithBuilder>()
            .HavingField(nameof(NameGenerator.FirstName),
                builder => builder.WithGenerator(typeof(NameGenerator.FirstName)))
            .HavingField(nameof(NameGenerator.LastName), _ => _.WithGenerator(typeof(NameGenerator.LastName)))
            .HavingField(nameof(BirthDate),
                _ => _.WithGenerator(typeof(DateGenerator.Past))
                    .WithConfiguration(new DateGenerator.PastConfig(50)))
            .HavingField(nameof(Slogan), _ => _.WithGenerator(typeof(LoremGenerator.Sentence)))
            .HavingField(nameof(PersonalDescription), _ => _.WithGenerator(typeof(LoremGenerator.Paragraph)))
            //.HavingField(nameof(DynamicResolvedGuid), _ => _.WithResolver(typeof(GuidResolver)))
            .HavingField(nameof(DynamicResolvedGuid), _ => _.WithStatic(() => GuidResolver.TestGuid))
            //.HavingField(nameof(DynamicResolvedString), _ => _.WithResolver(typeof(StringResolver)))
            .HavingField(nameof(DynamicResolvedString), _ => _.WithStatic(() => StringResolver.TestString))
            .Build();
    }
}