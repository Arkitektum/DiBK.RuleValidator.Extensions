using DiBK.RuleValidator;

namespace DiBK.RuleValidator.Extensions
{
    public static class RuleExtensions
    {
        public static void AddMessage(this Rule rule, string message)
        {
            rule.Messages.Add(new RuleMessage
            {
                Message = message
            });
        }

        public static void AddMessage(this Rule rule, string message, string fileName)
        {
            rule.Messages.Add(new RuleMessage
            {
                Message = message,
                FileName = fileName
            });
        }
    }
}




