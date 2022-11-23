using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Wmhelp.XPath2;

namespace DiBK.RuleValidator.Extensions
{
    public static class XPath2Extensions
    {
        private static readonly Regex _namespaceRegex = new(@"(?<prefix>\w+):(\w+|\*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Dictionary<string, XNamespace> _namespaces = new()
        {            
            { "gml", "http://www.opengis.net/gml/3.2" },
            { "xlink", "http://www.w3.org/1999/xlink" }
        };

        public static XElement GetElement(this XNode node, string xPath)
        {
            return node.XPath2SelectOne<XElement>(xPath, GetNamespaces(xPath));
        }

        public static IEnumerable<XElement> GetElements(this XNode node, string xPath)
        {
            var namespaces = GetNamespaces(xPath);
            return node.XPath2SelectElements(xPath, namespaces);
        }

        public static IEnumerable<XElement> GetElements(this IEnumerable<XElement> elements, string xPath)
        {
            return elements
                .SelectMany(element => GetElements(element, xPath))
                .Where(element => element != null);
        }

        public static string GetValue(this XElement element)
        {
            return element?.Value.Trim();
        }

        public static string GetValue(this XNode node, string xPath)
        {
            var element = GetElement(node, xPath);

            return GetValue(element);
        }

        public static T GetValue<T>(this XElement element) where T : IConvertible
        {
            var value = element?.Value.Trim();

            if (value == null)
                return default;

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T GetValue<T>(this XNode node, string xPath) where T : IConvertible
        {
            var element = GetElement(node, xPath);

            return GetValue<T>(element);
        }

        public static IEnumerable<string> GetValues(this IEnumerable<XElement> elements, string xPath)
        {
            return elements.Select(element => element.GetValue(xPath)).Where(value => value != null);
        }

        public static IEnumerable<T> GetValues<T>(this XNode node, string xPath) where T : IConvertible
        {
            return GetElements(node, xPath)
                .Select(element => element.GetValue<T>());
        }

        public static IEnumerable<T> GetValues<T>(this IEnumerable<XElement> elements, string xPath) where T : IConvertible
        {
            return elements.Select(element => element.GetValue<T>(xPath));
        }

        public static string GetAttribute(this XElement element, string attributeName)
        {
            var attrName = attributeName.Split(":");

            if (attrName.Length > 1 && _namespaces.TryGetValue(attrName[0], out XNamespace ns))
                return element?.Attribute(ns + attrName[1])?.Value;

            return element?.Attribute(attrName[0])?.Value;
        }

        public static IEnumerable<string> GetAttributes(this IEnumerable<XElement> elements, string attributeName)
        {
            var attrName = attributeName.Split(":");
            XName xName = attrName[0];

            if (attrName.Length > 1 && _namespaces.TryGetValue(attrName[0], out XNamespace ns))
                xName = ns + attrName[1];

            var attributes = new List<string>();

            foreach (var element in elements)
            {
                var value = element.Attribute(xName)?.Value;

                if (value != null)
                    attributes.Add(value);
            }

            return attributes;
        }

        public static string GetName(this XElement element)
        {
            return element?.Name.LocalName;
        }

        public static bool Has(this XNode node, string xPath)
        {
            return node.XPath2SelectElements(xPath, GetNamespaces(xPath)).Any();
        }

        public static string GetXPath(this XElement element, bool namespaces = true)
        {
            string elementName = element.Name.LocalName;

            if (namespaces)
            {
                var prefix = element.GetPrefixOfNamespace(element.Name.Namespace);

                if (!string.IsNullOrWhiteSpace(prefix))
                    elementName = $"{prefix}:{elementName}";
            }

            var path = "/" + elementName;
            var parentElement = element.Parent;

            if (parentElement != null)
            {
                var position = element.ElementsBeforeSelf(element.Name).Count() + 1;
                path = GetXPath(parentElement, namespaces) + $"{path}[{position}]";
            }
            else
            {
                path += "[1]";
            }

            return path;
        }

        private static XmlNamespaceManager GetNamespaces(string xPath)
        {
            var namespaceManager = new XmlNamespaceManager(new NameTable());

            foreach (Match match in _namespaceRegex.Matches(xPath))
            {
                var prefix = match.Groups["prefix"].Value;

                if (_namespaces.TryGetValue(prefix, out var xNamespace))
                    namespaceManager.AddNamespace(prefix, xNamespace.NamespaceName);
            }

            return namespaceManager;
        }
    }
}
