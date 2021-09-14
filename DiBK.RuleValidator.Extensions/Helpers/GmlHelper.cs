using System;
using System.Xml.Linq;

namespace DiBK.RuleValidator.Extensions
{
    public class GmlHelper
    {
        public static string GetFeatureType(XElement element)
        {
            return GetFeature(element)?.GetName();
        }

        public static XElement GetFeature(XElement element)
        {
            while (element.Parent != null && element.Parent.Name.LocalName != "featureMember" && element.Parent.Name.LocalName != "featureMembers")
                element = element.Parent;

            return element;
        }

        public static XElement GetClosestGmlElement(XElement element)
        {
            if (element.GetAttribute("gml:id") != null)
                return element;

            return element.GetElement("/ancestor::*[@gml:id][last()]");
        }

        public static string GetClosestGmlId(XElement element)
        {
            return GetClosestGmlElement(element)?.GetAttribute("gml:id");
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

            return $"{feature.GetName()} '{feature.GetAttribute("gml:id")}'";
        }

        public static int GetDimensions(XElement element)
        {
            var dimensions = element.GetElement("//*[@srsDimension][1]").GetAttribute("srsDimension");

            return Convert.ToInt32(dimensions);
        }
    }
}
