namespace CleanArchitecture.Application.Resources.PasswordEntropy
{
    public sealed class PoorPasswordEntropy : PasswordEntropy
    {
        public PoorPasswordEntropy(int entropyValue) : base(entropyValue)
        {
            IsValid = false;
            SecurityLevel = "Ruim";
        }
    }
}
