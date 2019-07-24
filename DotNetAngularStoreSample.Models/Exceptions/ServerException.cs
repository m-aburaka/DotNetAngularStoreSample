namespace DotNetAngularStoreSample.Models.Exceptions
{
    /// <summary>
    /// Base for all exceptions, which should be returned to the client
    /// </summary>
    public abstract class ServerException : SerializableException
    {
        public virtual int Code { get; private set; }

        public ServerException()
        {

        }

        public ServerException(string message, string stackTrace, SerializableException innerException) : base(message, stackTrace, innerException)
        {

        }
    }
}