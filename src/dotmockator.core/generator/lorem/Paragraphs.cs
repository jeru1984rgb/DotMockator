using Bogus.DataSets;

namespace dotmockator.core.generator.lorem;

public partial class LoremGenerator
{
    public class Paragraph : FieldGenerator
    {
        public Paragraph() : base(_ => new Lorem().Paragraphs(separator: Environment.NewLine))
        {
        }
    }
}