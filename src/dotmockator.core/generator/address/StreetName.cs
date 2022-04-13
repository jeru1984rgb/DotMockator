using Bogus.DataSets;

namespace dotmockator.core.generator.address;

public partial class AddressGenerator
{
    public class StreetName : FieldGenerator
    {
        public StreetName() : base(_ => new Address().StreetName())
        {
        }
    }
}