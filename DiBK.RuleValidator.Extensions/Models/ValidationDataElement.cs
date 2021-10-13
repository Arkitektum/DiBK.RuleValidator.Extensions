using System;
using System.IO;
using System.Xml.Linq;

namespace DiBK.RuleValidator.Extensions
{
    public class ValidationDataElement<T> where T : class
    {
        public string Id { get; } = Guid.NewGuid().ToString();
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

        public ValidationDataElement(T @class, XDocument document, string fileName, object dataType)
        {
            Class = @class;
            Document = document;
            FileName = fileName;
            DataType = dataType?.ToString();
        }

        public static ValidationDataElement<T> Create(InputData inputData)
        {
            var @class = XmlHelper.DeserializeXML<T>(inputData.Stream);
            
            inputData.Stream.Seek(0, SeekOrigin.Begin);
            
            var document = XDocument.Load(inputData.Stream);
            
            return new ValidationDataElement<T>(@class, document, inputData.FileName, inputData.DataType);
        }
    }
}
