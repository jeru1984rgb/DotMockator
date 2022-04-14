namespace DotMockator.Core.Generator.Name;

public partial class NameGenerator
{
    public class FirstName : FieldGenerator
    {
        public FirstName() : base(_ => new Bogus.DataSets.Name().FirstName())
        {
        }
    }
}