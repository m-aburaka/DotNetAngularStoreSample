namespace DotNetAngularStoreSample.Models.Exceptions
{
    public class BadRequestException : ServerException
    {
        public override string Message { get; }
        public override int Code => 400;

        public BadRequestException(string message)
        {
            Message = message;
        }

        public BadRequestException(string message, string stackTrace, SerializableException innerException) : base(message, stackTrace, innerException)
        {
            Message = message;
        }
    }
}