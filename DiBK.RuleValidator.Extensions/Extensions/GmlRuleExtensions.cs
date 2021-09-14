using DiBK.RuleValidator.Models;
using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public static class GmlRuleExtensions
    {
        public static void AddMessage(this Rule rule, string message, string fileName, IEnumerable<string> xPaths, IEnumerable<string> gmlIds)
        {
            rule.Messages.Add(new GmlRuleMessage
            {
                Message = message,
                FileName = fileName,
                XPath = xPaths,
                GmlIds = gmlIds,
            });
        }

        public static void AddMessage(this Rule rule, string message, string fileName, IEnumerable<string> xPaths, IEnumerable<string> gmlIds, string zoomTo)
        {
            rule.Messages.Add(new GmlRuleMessage
            {
                Message = message,
                FileName = fileName,
                XPath = xPaths,
                GmlIds = gmlIds,
                ZoomTo = zoomTo
            });
        }
    }
}