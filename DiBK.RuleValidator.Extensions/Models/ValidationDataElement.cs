using System;
using System.Xml.Linq;

namespace DiBK.RuleValidator.Extensions
{
    public class ValidationDataElement
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public XDocument Document { get; private set; }
        public string FileName { get; private set; }
        public string DataType { get; private set; }


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

        public static ValidationDataElement Create(InputData inputData)
        {
            var document = XDocument.Load(inputData.Stream);

            return new ValidationDataElement(document, inputData.FileName, inputData.DataType);
        }
    }

    public class ValidationDataElement<T> : ValidationDataElement where T : class
    {
        public T Class { get; private set; }

        public ValidationDataElement(T @class, XDocument document, string fileName) : base(document, fileName)
        {
            Class = @class;
        }

        public ValidationDataElement(T @class, XDocument document, string fileName, object dataType) : base(document, fileName, dataType)
        {
            Class = @class;
        }

        public static new ValidationDataElement<T> Create(InputData inputData)
        {
            var @class = XmlHelper.DeserializeXML<T>(inputData.Stream);

            inputData.Stream.Position = 0;
            
            var document = XDocument.Load(inputData.Stream);
            
            return new ValidationDataElement<T>(@class, document, inputData.FileName, inputData.DataType);
        }
    }
}
