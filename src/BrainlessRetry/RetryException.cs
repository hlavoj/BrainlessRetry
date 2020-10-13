using System;
using System.Runtime.Serialization;

namespace BrainlessRetry
{
    public class RetryException : Exception
    {
        public RetryException()
        {
        }

        protected RetryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RetryException(string message) : base(message)
        {
        }

        public RetryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
