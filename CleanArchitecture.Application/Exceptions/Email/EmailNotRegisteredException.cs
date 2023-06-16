namespace CleanArchitecture.Application.Exceptions.Email
{
    public class EmailNotRegisteredException : EmailException
    {
        public EmailNotRegisteredException(string message) : base(message)
        { }
    }
}
