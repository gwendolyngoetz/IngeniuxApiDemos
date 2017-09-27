using System;
using System.Runtime.Serialization;

namespace IngeniuxApiDemos.Common.Exceptions
{
    [Serializable]
    public class DataImportException : Exception
    {
        public DataImportException() { }
        public DataImportException(string message) : base(message) { }
        public DataImportException(string message, Exception inner) : base(message, inner) { }
        protected DataImportException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
