using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Resources.Password;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Persistence.Services
{
    public class PasswordService : IPasswordService
    {
        private const int DEGREE_OF_PARALLELISM = 8;
        private const int ITERATIONS = 4;
        private const int MEMORY_SIZE = 1024 * 128;

        public Task<PasswordEntropy> CalculateEntropy(string password)
        {
            int lowercaseLetters = password.Any(char.IsLower) ? 26 : 0;
            int upercaseLetters = password.Any(char.IsUpper) ? 26 : 0;
            int specialCharacters = password.Any(ch => !char.IsLetterOrDigit(ch)) ? 32 : 0;
            int numbers = password.Any(char.IsDigit) ? 10 : 0;

            int L = password.Length;
            int R = lowercaseLetters + upercaseLetters + specialCharacters + numbers;
            double entropyValue = L * Math.Log2(R);

            return Task.Run(() => new PasswordEntropy(true, entropyValue, "Top"));
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
