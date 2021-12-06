using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public static class GmlRuleExtensions
    {
        public static void AddMessage(this ExecutableRule rule, string message, string fileName, IEnumerable<string> xPaths, IEnumerable<string> gmlIds)
        {
            rule.AddMessage(new GmlRuleMessage
            {
                Message = message,
                FileName = fileName,
                XPath = xPaths,
                GmlIds = gmlIds,
            });
        }

        public static void AddMessage(this ExecutableRule rule, string message, string fileName, IEnumerable<string> xPaths, IEnumerable<string> gmlIds, string zoomTo)
        {
            rule.AddMessage(new GmlRuleMessage
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