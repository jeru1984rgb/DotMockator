using System;
using DotMockator.Core.Definitions;
using DotMockator.Core.Definitions.Builder;
using DotMockator.Core.Generator.Date;
using DotMockator.Core.Generator.Lorem;
using DotMockator.Core.Generator.Name;

namespace DotMockator.Core.Test.TestData.Person;

public class PersonMockDefinitionWithBuilder : IPersonMockDefinition
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public DateTime? BirthDate { get; set; }
    public string? Slogan { get; set; }
    public string? PersonalDescription { get; set; }
    public Guid? DynamicResolvedGuid { get; set; }
    public string? DynamicResolvedString { get; set; }

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
            .HavingField(nameof(DynamicResolvedGuid), _ => _.WithResolver(typeof(GuidResolver)))
            .HavingField(nameof(DynamicResolvedString), _ => _.WithResolver(typeof(StringResolver)))
            .Build();
    }
}