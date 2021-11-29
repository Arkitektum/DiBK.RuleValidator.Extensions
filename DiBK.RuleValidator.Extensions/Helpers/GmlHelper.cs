using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DiBK.RuleValidator.Extensions
{
    public class GmlHelper
    {
        private static readonly Regex _srsNameRegex = 
            new(@"^(http:\/\/www\.opengis\.net\/def\/crs\/EPSG\/0\/|^urn:ogc:def:crs:EPSG::)(?<epsg>\d+)$", RegexOptions.Compiled);

        private static readonly XNamespace _gmlNs = "http://www.opengis.net/gml/3.2";

        public static string GetFeatureType(XElement element)
        {
            return GetFeature(element)?.GetName();
        }

        public static XElement GetBaseGmlElement(XElement element)
        {
            return element.AncestorsAndSelf()
                .FirstOrDefault(element => element.Parent.Name.Namespace != _gmlNs);
        }

        public static XElement GetFeature(XElement element)
        {
            return element.AncestorsAndSelf()
                .FirstOrDefault(element => element.Parent.Name.LocalName == "featureMember" || element.Parent.Name.LocalName == "featureMembers");
        }

        public static string GetFeatureGmlId(XElement element)
        {
            return GetFeature(element)?.Attribute(_gmlNs + "id")?.Value;
        }

        public static XElement GetClosestGmlIdElement(XElement element)
        {
            return element.AncestorsAndSelf()
                .FirstOrDefault(element => element.Attribute(_gmlNs + "id") != null);
        }

        public static string GetClosestGmlId(XElement element)
        {
            return GetClosestGmlIdElement(element)?.Attribute(_gmlNs + "id")?.Value;
        }

        public static XLink GetXLink(XElement element)
        {
            if (element == null)
                return null;

            var xlink = element.GetAttribute("xlink:href")?.Split("#") ?? Array.Empty<string>();

            if (xlink.Length != 2)
                return null;

            var fileName = !string.IsNullOrWhiteSpace(xlink[0]) ? xlink[0] : null;
            var gmlId = !string.IsNullOrWhiteSpace(xlink[1]) ? xlink[1] : null;

            return new XLink(fileName, gmlId);
        }

        public static string GetContext(XElement element)
        {
            var feature = GetFeature(element);

            return GetNameAndId(feature);
        }

        public static string GetNameAndId(XElement element)
        {
            var gmlId = element.Attribute(_gmlNs + "id")?.Value;

            return $"{element.GetName()}{(!string.IsNullOrWhiteSpace(gmlId) ? $" '{gmlId}'" : "")}";
        }

        public static int GetDimensions(GmlDocument document)
        {
            var dimensions = document.Document.Root.GetElement("*:boundedBy/*:Envelope").GetAttribute("srsDimension");

            return Convert.ToInt32(dimensions);
        }

        public static int? GetEpsgCode(string srsName)
        {
            var match = _srsNameRegex.Match(srsName);

            if (!match.Success)
                return null;

            return int.Parse(match.Groups["epsg"].Value);
        }
    }
}
