namespace CleanArchitecture.Application.Exceptions.Claims
{
    public class ClaimException : Exception
    {
        public ClaimException(string message) : base(message) { }
    }
}
