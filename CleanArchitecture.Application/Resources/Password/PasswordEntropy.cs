namespace CleanArchitecture.Application.Resources.Password
{
    public sealed record PasswordEntropy(bool IsValid, double EntropyValue, string SecurityLevel);
}
