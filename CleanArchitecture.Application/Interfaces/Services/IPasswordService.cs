using CleanArchitecture.Application.Resources.Password;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IPasswordService
    {
        Task<PasswordEntropy> CalculateEntropy(string password);

        Task<byte[]> GenerateSaltAsync();
        Task<byte[]> CreateHashAsync(byte[] password, byte[] salt);
        Task<bool> VerifyPasswordAsync(byte[] passwordToCompare, byte[] salt, byte[] passwordHash);
    }
}
