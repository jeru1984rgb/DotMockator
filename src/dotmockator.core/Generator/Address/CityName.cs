namespace DotMockator.Core.Generator.Address;

public partial class AddressGenerator
{
    public class CityName : FieldGenerator
    {
        public CityName() : base(_ => new Bogus.DataSets.Address().City())
        {
        }
    }
}