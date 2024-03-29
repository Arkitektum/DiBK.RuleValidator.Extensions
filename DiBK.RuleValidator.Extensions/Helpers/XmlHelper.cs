﻿using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DiBK.RuleValidator.Extensions
{
    public class XmlHelper
    {
        public static T DeserializeXML<T>(Stream stream) where T : class
        {
            try
            {
                using var reader = XmlReader.Create(stream);
                reader.MoveToContent();

                var serializer = new XmlSerializer(typeof(T));

                return serializer.Deserialize(reader) as T;
            }
            catch
            {
                throw;
            }
        }

        public static (int LineNumber, int LinePosition) GetLineInfo(XObject @object)
        {
            IXmlLineInfo lineInfo = @object;

            return lineInfo.HasLineInfo() ? (lineInfo.LineNumber, lineInfo.LinePosition) : default;
        }
    }
}
