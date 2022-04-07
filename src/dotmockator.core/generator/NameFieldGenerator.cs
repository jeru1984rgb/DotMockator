using dotmockator.core.definitions;

namespace dotmockator.core.generator;

public static class NameFieldGenerator
{
    
    public static readonly FieldGenerator FirstNameGenerator = new(Faker.Name.First, FieldFunctionEnum.FirstName);
    public static readonly FieldGenerator LastNameGenerator = new(Faker.Name.Last, FieldFunctionEnum.LastName);
}