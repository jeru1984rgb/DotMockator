using Bogus.DataSets;
using dotmockator.core.definitions.field;

namespace dotmockator.core.generator.date;

public partial class DateGenerator
{
    public class Past : FieldGenerator
    {
        public Past() : base(def =>
        {
            var config = def.GetConfiguration<PastConfig>();
            return new Date().Past(config.YearsToGoBack);
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