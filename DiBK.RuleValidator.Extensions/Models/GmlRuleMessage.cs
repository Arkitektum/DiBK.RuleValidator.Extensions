using System;
using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public class GmlRuleMessage : XmlRuleMessage
    {
        public IEnumerable<string> GmlIds { get; set; } = Array.Empty<string>();
        public string ZoomTo { get; set; }
    }
}
