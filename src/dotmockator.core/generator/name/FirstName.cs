using Bogus.DataSets;

namespace dotmockator.core.generator.name;

public partial class NameGenerator
{
    public class FirstName : FieldGenerator
    {
        public FirstName() : base(_ => new Name().FirstName())
        {
        }
    }
}