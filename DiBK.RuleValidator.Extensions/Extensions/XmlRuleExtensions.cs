using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public static class XmlRuleExtensions
    {
        public static void AddMessage(this ExecutableRule rule, string message, string fileName, IEnumerable<string> xPaths)
        {
            rule.AddMessage(new XmlRuleMessage
            {
                Message = message,
                FileName = fileName,
                XPath = xPaths
            });              
        }
    }
}