using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using DotNetAngularStoreSample.Models.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace DotNetAngularStoreSample.Application.Services
{
    /// <summary>
    /// Basic logging and converting of exceptions to make them ready to be transferred back to client
    /// </summary>
    public class ExceptionHandlerService
    {
        private readonly ILogger _logger;

        public static JsonSerializerSettings SerializerSettings => new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
        };

        public ExceptionHandlerService(ILogger logger)
        {
            _logger = logger;
        }

        public string LogAndFormatToJson(Exception ex)
        {
            var serializable = MakeSerializableIfNot(ex);
            Log(serializable, _logger);

            return JsonConvert.SerializeObject(serializable, SerializerSettings);
        }

        private SerializableException MakeSerializableIfNot(Exception ex)
        {
            if (ex is SerializableException exception)
                return exception;

            SerializableException innerException = null;
            if (ex.InnerException != null)
                innerException = new SerializableException(ex.InnerException.Message, ex.InnerException.StackTrace, null);

            return new SerializableException(ex.Message, ex.StackTrace, innerException);
        }

        private void Log(SerializableException ex, ILogger serilogLogger)
        {
            var code = GetStatusCode(ex);
            if (code >= 400 && code < 500)
                serilogLogger?.Warning("{Code} {Exception}", code, ex);
            else
                serilogLogger?.Error(ex, "Server error");
        }

        public int GetStatusCode(Exception ex)
        {
            if (ex is ServerException serverException)
                return serverException.Code;
            else
                return (int)HttpStatusCode.InternalServerError;
        }
    }
}
