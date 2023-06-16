namespace CleanArchitecture.Application.Exceptions.Claims
{
    public sealed class EmailClaimException : ClaimException
    {
        public EmailClaimException(string message) : base(message)
        { }
    }
}
