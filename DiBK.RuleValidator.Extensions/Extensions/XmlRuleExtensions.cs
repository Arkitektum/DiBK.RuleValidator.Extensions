using DiBK.RuleValidator.Models;
using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public static class XmlRuleExtensions
    {
        public static void AddMessage(this Rule rule, string message, string fileName, IEnumerable<string> xPaths)
        {
            rule.Messages.Add(new XmlRuleMessage
            {
                Message = message,
                FileName = fileName,
                XPath = xPaths
            });
        }
    }
}