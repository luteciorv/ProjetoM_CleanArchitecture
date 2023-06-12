namespace CleanArchitecture.Application.Exceptions
{
    public sealed class UsernameAlreadyRegisteredException : BaseException
    {
        public UsernameAlreadyRegisteredException(string message) : base(message) { }
    }
}
