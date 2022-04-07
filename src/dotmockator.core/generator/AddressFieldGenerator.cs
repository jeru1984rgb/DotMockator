using dotmockator.core.definitions;

namespace dotmockator.core.generator;

public static class AddressFieldGenerator
{
    public static readonly FieldGenerator AddressCityNameGenerator = new(Faker.Address.City, FieldFunctionEnum.AddressCity);
}