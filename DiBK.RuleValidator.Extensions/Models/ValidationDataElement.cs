using System;
using System.Xml.Linq;

namespace DiBK.RuleValidator.Extensions
{
    public class ValidationDataElement<T>
    {
        public T Class { get; private set; }
        public XDocument Document { get; private set; }
        public string FileName { get; private set; }
        public string DataType { get; private set; }


        public ValidationDataElement(T @class, string fileName)
        {
            Class = @class;
            FileName = fileName;
        }

        public ValidationDataElement(T @class, XDocument document, string fileName)
        {
            Class = @class;
            Document = document;
            FileName = fileName;
        }

        public ValidationDataElement(XDocument document, string fileName)
        {
            Document = document;
            FileName = fileName;
        }

        public ValidationDataElement(XDocument document, string fileName, object dataType)
        {
            Document = document;
            FileName = fileName;
            DataType = dataType?.ToString();
        }

        public ValidationDataElement(T @class, string fileName, object dataType)
        {
            Class = @class;
            FileName = fileName;
            DataType = dataType?.ToString();
        }

        public ValidationDataElement(T @class, XDocument document, string fileName, Enum dataType)
        {
            Class = @class;
            Document = document;
            FileName = fileName;
            DataType = dataType?.ToString();
        }
    }
}
