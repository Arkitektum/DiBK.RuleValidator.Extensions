namespace DiBK.RuleValidator.Extensions
{
    public class CodelistItem
    {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public string Description { get; private set; }

        public CodelistItem(string name, string value, string description)
        {
            Name = name;
            Value = value;
            Description = description;
        }
    }
}
