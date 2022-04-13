using System;

namespace dotmockator.core.test.testdata.person;

public interface IPersonMockDefinition
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string Slogan { get; set; }

    public string PersonalDescription { get; set; }

    public Guid DynamicResolvedGuid { get; set; }

    public string DynamicResolvedString { get; set; }
}