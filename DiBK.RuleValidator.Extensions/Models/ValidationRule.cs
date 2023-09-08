using System.Collections.Generic;

namespace DiBK.RuleValidator.Extensions
{
    public class ValidationRule
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Documentation { get; set; }
        public string MessageType { get; set; }
        public double TimeUsed { get; set; }
        public List<Dictionary<string, object>> Messages { get; set; } = new();
    }
}
