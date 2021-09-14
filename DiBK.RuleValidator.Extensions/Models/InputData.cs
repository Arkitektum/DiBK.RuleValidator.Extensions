using System;
using System.IO;

namespace DiBK.RuleValidator.Extensions
{
    public class InputData : IDisposable
    {
        public InputData(Stream stream, string fileName)
        {
            Stream = stream;
            FileName = fileName;
        }

        public Stream Stream { get; private set; }
        public string FileName { get; private set; }
        public bool IsValid { get; set; } = true;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing || Stream == null)
                return;

            Stream.Dispose();
        }
    }
}
