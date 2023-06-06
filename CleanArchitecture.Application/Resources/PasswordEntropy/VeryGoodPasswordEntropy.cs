namespace CleanArchitecture.Application.Resources.PasswordEntropy
{
    public sealed class VeryGoodPasswordEntropy : PasswordEntropy
    {
        public VeryGoodPasswordEntropy(int entropyValue) : base(entropyValue)
        {
            IsValid = true;
            SecurityLevel = "Muito boa";
        }
    }
}
