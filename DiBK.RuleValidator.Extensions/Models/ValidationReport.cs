using System;
using System.Collections.Generic;
using System.Linq;

namespace DiBK.RuleValidator.Extensions
{
    public class ValidationReport
    {
        public string CorrelationId { get; set; }
        public string Namespace { get; set; }
        public int Errors { get; set; }
        public int Warnings { get; set; }
        public List<ValidationRule> Rules { get; set; } = new();
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<string> Files { get; set; }
        public double TimeUsed => Math.Round(EndTime.Subtract(StartTime).TotalSeconds, 2);

        public static ValidationReport Create(
            object correlationId, List<Rule> rules, DisposableList<InputData> inputData, List<string> xmlNamespaces, DateTime startTime)
        {
            return Create(correlationId, rules, inputData.Select(data => data.FileName), xmlNamespaces, startTime);
        }

        public static ValidationReport Create(
            object correlationId, List<Rule> rules, DisposableList<InputData> inputData, DateTime startTime)
        {
            return Create(correlationId, rules, inputData.Select(data => data.FileName), startTime);
        }

        public static ValidationReport Create(
            object correlationId, List<Rule> rules, IEnumerable<string> fileNames, DateTime startTime)
        {
            return Create(correlationId, rules, fileNames, null, startTime);
        }

        public static ValidationReport Create(
            object correlationId, List<Rule> rules, IEnumerable<string> fileNames, List<string> xmlNamespaces, DateTime startTime)
        {
            return new ValidationReport
            {
                CorrelationId = correlationId as string,
                Namespace = string.Join(", ", xmlNamespaces),
                Errors = rules
                    .Where(rule => rule.Status == Status.FAILED)
                    .SelectMany(rule => rule.Messages)
                    .Count(),
                Warnings = rules
                    .Where(rule => rule.Status == Status.WARNING)
                    .SelectMany(rule => rule.Messages)
                    .Count(),
                Rules = rules
                    .ConvertAll(rule =>
                    {
                        return new ValidationRule
                        {
                            Id = rule.Id,
                            Name = rule.Name,
                            Description = rule.Description,
                            Documentation = rule.Documentation,
                            Status = rule.Status.ToString(),
                            TimeUsed = rule.TimeUsed,
                            MessageType = rule.MessageType.ToString(),
                            Messages = rule.Messages
                                .Select(message =>
                                {
                                    var messageDictionary = new Dictionary<string, object> { { "Message", message.Message } };

                                    if (message.Properties != null)
                                        messageDictionary.Append(message.Properties);

                                    return messageDictionary;
                                })
                                .ToList(),
                        };
                    }),
                StartTime = startTime,
                EndTime = DateTime.Now,
                Files = fileNames.ToList()
            };
        }
    }
}
