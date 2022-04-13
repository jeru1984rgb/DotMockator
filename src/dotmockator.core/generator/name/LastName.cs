using Bogus.DataSets;

namespace dotmockator.core.generator.name;

public partial class NameGenerator
{
    public class LastName : FieldGenerator
    {
        public LastName() : base(_ => new Name().LastName())
        {
        }
    }
}