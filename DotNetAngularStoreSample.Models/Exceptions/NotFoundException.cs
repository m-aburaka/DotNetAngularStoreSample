namespace DotNetAngularStoreSample.Models.Exceptions
{
    public class NotFoundException : ServerException
    {
        public override int Code => 404;
        public override string Message { get; }

        public NotFoundException(string message)
        {
            Message = message;
        }

        public NotFoundException(string message, string stackTrace, SerializableException innerException) : base(message, stackTrace, innerException)
        {
            Message = message;
        }
    }
}