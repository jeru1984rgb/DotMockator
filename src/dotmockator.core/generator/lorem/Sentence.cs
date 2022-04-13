using Bogus.DataSets;

namespace dotmockator.core.generator.lorem;

public partial class LoremGenerator
{
    public class Sentence : FieldGenerator
    {
        public Sentence() : base(_ => new Lorem().Sentence())
        {
        }
    }
}