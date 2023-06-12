namespace CleanArchitecture.Application.Exceptions.Email
{
    public abstract class EmailException : BaseException
    {
        public EmailException(string message) : base(message) { }
    }
}
