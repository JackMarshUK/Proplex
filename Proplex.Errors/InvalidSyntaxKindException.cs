using System;
using System.Runtime.Serialization;

namespace Proplex.Errors
{
    [Serializable]
    public class InvalidSyntaxKindException : Exception
    {
        public InvalidSyntaxKindException()
        {
        }

        public InvalidSyntaxKindException(string message) : base(message)
        {
        }

        public InvalidSyntaxKindException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSyntaxKindException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}