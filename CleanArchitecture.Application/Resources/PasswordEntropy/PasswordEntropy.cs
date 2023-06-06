namespace CleanArchitecture.Application.Resources.PasswordEntropy
{
    public abstract class PasswordEntropy
    {
        protected PasswordEntropy(int entropyValue) =>
            EntropyValue = entropyValue;

        public bool IsValid { get; protected set; }
        public int EntropyValue { get; private set; }
        public string SecurityLevel { get; protected set; } = string.Empty;
    }
}
