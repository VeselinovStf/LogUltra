using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LogUltra.Log.Service.Exceptions
{
    public class LogServiceException : Exception
    {
        public LogServiceException()
        {
        }

        public LogServiceException(string message) : base(message)
        {
        }

        public LogServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LogServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
