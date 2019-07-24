using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetAngularStoreSample.Models.Exceptions
{
    /// <summary>
    /// Base for all exceptions to be returned to client.
    /// Made to keep the original message, stacktrace and inner exception, while stripping out unnecessary data, which can be to big to transfer
    /// </summary>
    public class SerializableException : Exception
    {
        private readonly string _stackTrace;

        public override string StackTrace => _stackTrace ?? base.StackTrace;

        public SerializableException()
        {

        }

        public SerializableException(
            string message,
            string stackTrace,
            Exception innerException) : base(message, innerException)
        {
            _stackTrace = stackTrace;
        }
    }
}
