namespace DotMockator.Core.Generator.Name;

public partial class NameGenerator
{
    public class LastName : FieldGenerator
    {
        public LastName() : base(_ => new Bogus.DataSets.Name().LastName())
        {
        }
    }
}