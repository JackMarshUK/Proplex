using System;
using System.Runtime.Serialization;

namespace Proplex.Errors
{
    [Serializable]
    public class InvalidSyntaxNodeException : Exception
    {
        public InvalidSyntaxNodeException()
        {
        }

        public InvalidSyntaxNodeException(string message) : base(message)
        {
        }

        public InvalidSyntaxNodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSyntaxNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}