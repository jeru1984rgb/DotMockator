using dotmockator.core.definitions;

namespace dotmockator.core.test;

public class SimpleMockDefinition
{
    [MockatorField(FieldFunctionEnum.FirstName)]
    public string FirstName { get; set; }

    [MockatorField(FieldFunctionEnum.LastName)]
    public string LastName { get; set; }
    
    public string NotMockedField { get; set; }
}