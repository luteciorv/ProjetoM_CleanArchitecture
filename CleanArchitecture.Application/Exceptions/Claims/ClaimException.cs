namespace CleanArchitecture.Application.Exceptions.Claims
{
    public abstract class ClaimException : Exception
    {
        public ClaimException(string message) : base(message) { }
    }
}
