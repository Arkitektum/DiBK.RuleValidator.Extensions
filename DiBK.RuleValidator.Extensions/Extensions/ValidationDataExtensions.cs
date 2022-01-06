using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DiBK.RuleValidator.Extensions
{
    public static class ValidationDataExtensions
    {
        public static XElement GetElement(this ValidationDataElement dataElement, string xPath)
        {
            return XPath2Extensions.GetElement(dataElement.Document, xPath);
        }

        public static IEnumerable<XElement> GetElements(this ValidationDataElement dataElement, string xPath)
        {
            return XPath2Extensions.GetElements(dataElement.Document, xPath);
        }

        public static T GetValue<T>(this ValidationDataElement dataElement, string xPath) where T : IConvertible
        {
            return XPath2Extensions.GetValue<T>(dataElement.Document, xPath);
        }

        public static string GetValue(this ValidationDataElement dataElement, string xPath)
        {
            return XPath2Extensions.GetValue(dataElement.Document, xPath);
        }

        public static IEnumerable<T> GetValues<T>(this ValidationDataElement dataElement, string xPath) where T : IConvertible
        {
            return XPath2Extensions.GetValues<T>(dataElement.Document, xPath);
        }

        public static IEnumerable<string> GetValues(this ValidationDataElement dataElement, string xPath)
        {
            return XPath2Extensions.GetValues(GetElements(dataElement, xPath), xPath);
        }
    }
}
