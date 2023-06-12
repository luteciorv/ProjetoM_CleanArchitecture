using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Exceptions.PasswordEntropy
{
    public sealed class WeakPasswordEntropyException : PasswordEntropyException
    {
        public WeakPasswordEntropyException(string message) : base(message)
        {
            SecurityLevel = EPasswordSecurityLevel.Weak;
        }
    }
}
