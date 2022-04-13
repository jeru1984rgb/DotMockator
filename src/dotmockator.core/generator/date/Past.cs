using Bogus.DataSets;
using dotmockator.core.definitions;

namespace dotmockator.core.generator.date;

public partial class DateGenerator
{
    public class Past : FieldGenerator
    {
        public Past() : base((def) =>
        {
            var config = def.GetGeneratorConfig<PastConfig>();
            if (config == null)
            {
                config = new PastConfig(50);
            }

            return new Date().Past(config.YearsToGoBack);
        })
        {
        }
    }

    public class PastConfig : MockatorGeneratorConfigAttribute, IConfiguration
    {
        public int YearsToGoBack { get; }

        public PastConfig(int yearsToGoBack)
        {
            YearsToGoBack = yearsToGoBack;
        }
    }
}