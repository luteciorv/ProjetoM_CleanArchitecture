using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Resources.PasswordEntropy;
using CleanArchitecture.Domain.Enums;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;

namespace CleanArchitecture.Persistence.Services
{
    public class PasswordService : IPasswordService
    {
        private const int DEGREE_OF_PARALLELISM = 8;
        private const int ITERATIONS = 4;
        private const int MEMORY_SIZE = 1024 * 128;

        /// <summary>
        ///     Calcula o nível de segurança de uma senha utilizando a entropia.
        ///     Mais informações no link: https://www.baeldung.com/cs/password-entropy
        /// </summary>
        public async Task<PasswordEntropy> CalculateEntropy(string password)
        {
            int lowercaseLetters = password.Any(char.IsLower) ? 26 : 0;
            int upercaseLetters = password.Any(char.IsUpper) ? 26 : 0;
            int specialCharacters = password.Any(ch => !char.IsLetterOrDigit(ch)) ? 32 : 0;
            int numbers = password.Any(char.IsDigit) ? 10 : 0;

            int L = password.Length;
            int R = lowercaseLetters + upercaseLetters + specialCharacters + numbers;
            int entropyValue = await Task.Run(() => Convert.ToInt32(L * Math.Log2(R)));

            if (0 <= entropyValue && entropyValue <= (int)EPasswordSecurityLevel.Poor)
                return new PoorPasswordEntropy(entropyValue);

            else if ((int)EPasswordSecurityLevel.Poor < entropyValue && entropyValue <= (int)EPasswordSecurityLevel.Weak)
                return new WeakPasswordEntropy(entropyValue);

            else if ((int)EPasswordSecurityLevel.Weak < entropyValue && entropyValue <= (int)EPasswordSecurityLevel.Resonable)
                return new ResonablePasswordEntropy(entropyValue);

            else
                return new VeryGoodPasswordEntropy(entropyValue);
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
