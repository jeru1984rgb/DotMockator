using DotMockator.Core.Definitions.Field;

namespace DotMockator.Core.Generator.Date;

public partial class DateGenerator
{
    public class Past : FieldGenerator
    {
        public Past() : base(def =>
        {
            var config = def.GetConfiguration<PastConfig>();
            return new Bogus.DataSets.Date().Past(config.YearsToGoBack);
        })
        {
        }
    }

    public class PastConfig : MockatorAttributeConfiguration
    {
        public PastConfig()
        {
        }

        public int YearsToGoBack { get; private set; }

        public PastConfig(int yearsToGoBack)
        {
            YearsToGoBack = yearsToGoBack;
        }

        public override void DefaultConfiguration()
        {
            YearsToGoBack = 10;
        }
    }
}