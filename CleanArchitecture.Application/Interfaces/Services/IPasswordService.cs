namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IPasswordService
    {
        /// <summary>
        ///     Garante que o nível de segurança de uma senha utilizando a entropia.
        ///     Mais informações no link: https://www.baeldung.com/cs/password-entropy
        /// </summary>
        Task EnsureEntropyIsValid(string password);

        Task<byte[]> GenerateSaltAsync();
        Task<byte[]> CreateHashAsync(byte[] password, byte[] salt);
        Task<bool> VerifyPasswordAsync(byte[] passwordToCompare, byte[] salt, byte[] passwordHash);
    }
}
