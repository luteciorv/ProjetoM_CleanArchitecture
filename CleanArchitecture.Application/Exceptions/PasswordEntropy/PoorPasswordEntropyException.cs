using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Exceptions.PasswordEntropy
{
    public sealed class PoorPasswordEntropyException : PasswordEntropyException
    {
        public PoorPasswordEntropyException(string message) : base(message)
        {
            SecurityLevel = EPasswordSecurityLevel.Poor;
        }
    }
}
