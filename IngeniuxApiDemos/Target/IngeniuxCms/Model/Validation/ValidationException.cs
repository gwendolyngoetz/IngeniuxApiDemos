using System;
using System.Runtime.Serialization;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationResults ValidationResults = new ValidationResults();

        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, ValidationResults validationResults) : this(message)
        {
            ValidationResults = validationResults;
        }
        public ValidationException(string message, Exception inner) : base(message, inner) { }
        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}