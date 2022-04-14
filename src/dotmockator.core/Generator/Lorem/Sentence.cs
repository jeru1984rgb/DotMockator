namespace DotMockator.Core.Generator.Lorem;

public partial class LoremGenerator
{
    public class Sentence : FieldGenerator
    {
        public Sentence() : base(_ => new Bogus.DataSets.Lorem().Sentence())
        {
        }
    }
}