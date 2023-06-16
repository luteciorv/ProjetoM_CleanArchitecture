namespace CleanArchitecture.Application.Exceptions.Email
{
    public class EmailAlreadyConfirmedException : EmailException
    {
        public EmailAlreadyConfirmedException(string message) : base(message)
        { }
    }
}
