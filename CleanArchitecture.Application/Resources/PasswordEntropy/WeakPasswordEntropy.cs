namespace CleanArchitecture.Application.Resources.PasswordEntropy
{
    public sealed class WeakPasswordEntropy : PasswordEntropy
    {
        public WeakPasswordEntropy(int entropyValue) : base(entropyValue)
        {
            IsValid = false;
            SecurityLevel = "Fraco";
        }
    }
}
