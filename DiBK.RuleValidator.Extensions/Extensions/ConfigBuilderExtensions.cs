using DiBK.RuleValidator.Models.Config;

namespace DiBK.RuleValidator.Extensions
{
    public static class ConfigBuilderExtensions
    {
        public static GroupOptionsBuilder WithUILocation(this GroupOptionsBuilder builder, string location)
        {
            builder.Group.Settings["UILocation"] = location;

            return builder;
        }

        public static RuleOptionsBuilder WithUILocation(this RuleOptionsBuilder builder, string location)
        {
            builder.Rule.Settings["UILocation"] = location;

            return builder;
        }
    }
}