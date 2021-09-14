using DiBK.RuleValidator;
using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public class XmlRuleMessage : RuleMessage
    {
        public IEnumerable<string> XPath { get; set; }
    }
}
