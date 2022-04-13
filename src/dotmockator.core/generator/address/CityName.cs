using Bogus.DataSets;

namespace dotmockator.core.generator.address;

public partial class AddressGenerator
{
    public class CityName : FieldGenerator
    {
        public CityName() : base(_ => new Address().City())
        {
        }
    }
}