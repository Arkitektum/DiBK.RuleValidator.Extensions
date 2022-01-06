using System;
using System.Collections.Generic;
using System.Linq;

namespace DiBK.RuleValidator.Extensions.Helpers
{
    public class ValidationHelper
    {
        public static ValidationReport CreateValidationReport(
            object correlationId, List<Rule> rules, DisposableList<InputData> inputData, DateTime startTime, string xmlNamespace = null)
        {
            return CreateValidationReport(correlationId, rules, inputData.Select(data => data.FileName), startTime, xmlNamespace);
        }

        public static ValidationReport CreateValidationReport(
            object correlationId, List<Rule> rules, IEnumerable<string> fileNames, DateTime startTime, string xmlNamespace = null)
        {
            return new ValidationReport
            {
                CorrelationId = correlationId as string,
                Namespace = xmlNamespace,
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
                            Messages = rule.Messages
                                .Select(message =>
                                {
                                    var messageDictionary = new Dictionary<string, object> { { "Message", message.Message } };
                                    messageDictionary.Append(message.Properties);

                                    return messageDictionary;
                                })
                                .ToList(),
                            MessageType = rule.MessageType.ToString(),
                            Status = rule.Status.ToString(),
                            PreCondition = rule.PreCondition,
                            ChecklistReference = rule.ChecklistReference,
                            Description = rule.Description,
                            Source = rule.Source,
                            Documentation = rule.Documentation
                        };
                    }),
                StartTime = startTime,
                EndTime = DateTime.Now,
                Files = fileNames.ToList()
            };
        }
    }
}
