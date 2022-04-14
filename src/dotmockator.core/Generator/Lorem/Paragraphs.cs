namespace DotMockator.Core.Generator.Lorem;

public partial class LoremGenerator
{
    public class Paragraph : FieldGenerator
    {
        public Paragraph() : base(_ => new Bogus.DataSets.Lorem().Paragraphs(separator: Environment.NewLine))
        {
        }
    }
}