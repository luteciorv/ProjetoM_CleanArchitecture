using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Exceptions.PasswordEntropy
{
    public class PasswordEntropyException : BaseException
    {
        public PasswordEntropyException(string message) : base(message) { }

        public EPasswordSecurityLevel SecurityLevel { get; protected set; }
    }
}
