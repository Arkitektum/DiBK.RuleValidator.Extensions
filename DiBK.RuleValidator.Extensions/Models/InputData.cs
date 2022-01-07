using System;
using System.IO;

namespace DiBK.RuleValidator.Extensions
{
    public class InputData : IDisposable
    {
        private bool _disposed = false;
        public Stream Stream { get; private set; }
        public string FileName { get; private set; }
        public object DataType { get; private set; }
        public bool IsValid { get; set; } = true;

        public InputData(Stream stream, string fileName, object dataType)
        {
            Stream = stream;
            FileName = fileName;
            DataType = dataType;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && Stream != null)
                    Stream.Dispose();

                _disposed = true;
            }
        }
    }
}
