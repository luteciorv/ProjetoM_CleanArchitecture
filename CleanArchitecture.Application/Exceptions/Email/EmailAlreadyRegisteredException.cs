namespace CleanArchitecture.Application.Exceptions.Email
{
    public class EmailAlreadyRegisteredException : EmailException
    {
        public EmailAlreadyRegisteredException(string message) : base(message) { }
    }
}
