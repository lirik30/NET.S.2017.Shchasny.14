using System;
using System.Runtime.Serialization;

namespace MatrixLogic
{
    public class NotSupportedException : Exception
    {
        public NotSupportedException()
        {
        }

        public NotSupportedException(string message) : base(message)
        {
        }

        public NotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
