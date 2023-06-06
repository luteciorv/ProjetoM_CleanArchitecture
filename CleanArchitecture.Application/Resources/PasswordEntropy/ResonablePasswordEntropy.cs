namespace CleanArchitecture.Application.Resources.PasswordEntropy
{
    public sealed class ResonablePasswordEntropy : PasswordEntropy
    {
        public ResonablePasswordEntropy(int entropyValue) : base(entropyValue)
        {
            IsValid = true;
            SecurityLevel = "Razoável";
        }
    }
}
