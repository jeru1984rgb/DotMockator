namespace DotMockator.Core.Generator.Address;

public partial class AddressGenerator
{
    public class StreetName : FieldGenerator
    {
        public StreetName() : base(_ => new Bogus.DataSets.Address().StreetName())
        {
        }
    }
}