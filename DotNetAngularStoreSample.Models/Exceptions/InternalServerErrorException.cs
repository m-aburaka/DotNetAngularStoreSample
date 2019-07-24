namespace DotNetAngularStoreSample.Models.Exceptions
{
    public class InternalServerErrorException : ServerException
    {
        public override int Code => 500;

        public InternalServerErrorException(string message, string stackTrace, SerializableException innerException) : base(message, stackTrace, innerException)
        {

        }
    }
}