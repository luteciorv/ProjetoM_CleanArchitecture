using CleanArchitecture.Application.Exceptions.PasswordEntropy;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Enums;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;

namespace CleanArchitecture.Infraestructure.Security.Services
{
    public class PasswordService : IPasswordService
    {
        private const int DEGREE_OF_PARALLELISM = 8;
        private const int ITERATIONS = 4;
        private const int MEMORY_SIZE = 1024 * 128;

        public async Task EnsureEntropyIsValid(string password)
        {
            int lowercaseLetters = password.Any(char.IsLower) ? 26 : 0;
            int upercaseLetters = password.Any(char.IsUpper) ? 26 : 0;
            int specialCharacters = password.Any(ch => !char.IsLetterOrDigit(ch)) ? 32 : 0;
            int numbers = password.Any(char.IsDigit) ? 10 : 0;

            int L = password.Length;
            int R = lowercaseLetters + upercaseLetters + specialCharacters + numbers;
            int entropyValue = await Task.Run(() => Convert.ToInt32(L * Math.Log2(R)));

            if (0 <= entropyValue && entropyValue <= (int)EPasswordSecurityLevel.Poor)
                throw new PoorPasswordEntropyException("A senha informada é muito fraca. Considere aumentar sua complexidade.");

            else if ((int)EPasswordSecurityLevel.Poor < entropyValue && entropyValue <= (int)EPasswordSecurityLevel.Weak)
                throw new WeakPasswordEntropyException("A senha informada é fraca. Considere aumentar a sua complexidade.");
        }

        public Task<byte[]> CreateHashAsync(byte[] password, byte[] salt)
        {
            using var argon2 = new Argon2id(password)
            {
                Salt = salt,
                DegreeOfParallelism = DEGREE_OF_PARALLELISM,
                Iterations = ITERATIONS,
                MemorySize = MEMORY_SIZE
            };

            return Task.Run(() => argon2.GetBytes(32));
        }

        public Task<byte[]> GenerateSaltAsync()
        {
            var buffer = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(buffer);

            return Task.Run(() => buffer);
        }

        public async Task<bool> VerifyPasswordAsync(byte[] password, byte[] salt, byte[] passwordToCompare) =>
            (await CreateHashAsync(password, salt)).SequenceEqual(passwordToCompare);
    }
}
