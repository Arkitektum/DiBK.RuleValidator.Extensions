using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public static class XmlRuleExtensions
    {
        public static void AddMessage(this ExecutableRule rule, string message, string fileName, IEnumerable<string> xPaths)
        {
            rule.AddMessage(new RuleMessage
            {
                Message = message,
                Properties = new Dictionary<string, object>
                {
                    { "FileName", fileName },
                    { "XPaths", xPaths },
                }
            });
        }
    }
}