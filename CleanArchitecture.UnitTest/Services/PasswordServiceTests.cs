using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Persistence.Services;
using System.Text;

namespace CleanArchitecture.UnitTest.Services
{
    [TestClass]
    public class PasswordServiceTests
    {
        private readonly IPasswordService _passwordService;
        public PasswordServiceTests()
        {
            _passwordService = new PasswordService();
        }

        [TestMethod]
        [DataRow("S3Nh@CoMpL3x@", "S3Nh@CoMpL3x@")]
        public async Task Dado_uma_senha_deve_hashear_ela(string password, string passwordToCompare)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var salt = await _passwordService.GenerateSaltAsync();
            var passwordHash = await _passwordService.CreateHashAsync(passwordBytes, salt); // Armazenar no banco de dados

            var passwordToCompareBytes = Encoding.UTF8.GetBytes(passwordToCompare);

            Assert.IsTrue(await _passwordService.VerifyPasswordAsync(passwordToCompareBytes, salt, passwordHash));
        }

        [TestMethod]
        [DataRow("200907rjJRV")]
        public async Task Dado_uma_senha_deve_ter_uma_entropia_boa(string password)
        {
            var entropy = await _passwordService.CalculateEntropy(password);
            Assert.IsTrue(entropy.IsValid);
        }
    }
}
