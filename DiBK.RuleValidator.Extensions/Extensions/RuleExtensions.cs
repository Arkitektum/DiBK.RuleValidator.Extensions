using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public static class RuleExtensions
    {
        public static void AddMessage(this ExecutableRule rule, string message)
        {
            rule.AddMessage(new RuleMessage
            {
                Message = message
            });
        }

        public static void AddMessage(this ExecutableRule rule, string message, string fileName)
        {
            rule.AddMessage(new RuleMessage
            {
                Message = message,
                Properties = new Dictionary<string, object>
                {
                    { "FileName", fileName }
                }
            });
        }
    }
}




