using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Resources.PasswordEntropy;
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
        [DataRow("070800")]
        public async Task Dado_uma_senha_deve_possuir_uma_entropia_ruim(string password)
        {
            var passwordEntropy = await _passwordService.CalculateEntropy(password);
            Assert.IsInstanceOfType(passwordEntropy, typeof(PoorPasswordEntropy));
        }

        [TestMethod]
        [DataRow("TeStEdOs")]
        public async Task Dado_uma_senha_deve_possuir_uma_entropia_fraca(string password)
        {
            var passwordEntropy = await _passwordService.CalculateEntropy(password);
            Assert.IsInstanceOfType(passwordEntropy, typeof(WeakPasswordEntropy));
        }

        [TestMethod]
        [DataRow("TeStE@12")]
        public async Task Dado_uma_senha_deve_possuir_uma_entropia_razoavel(string password)
        {
            var passwordEntropy = await _passwordService.CalculateEntropy(password);
            Assert.IsInstanceOfType(passwordEntropy, typeof(ResonablePasswordEntropy));
        }

        [TestMethod]
        [DataRow("TeStE@147258")]
        public async Task Dado_uma_senha_deve_possuir_uma_entropia_muito_boa(string password)
        {
            var passwordEntropy = await _passwordService.CalculateEntropy(password);
            Assert.IsInstanceOfType(passwordEntropy, typeof(VeryGoodPasswordEntropy));
        }
    }
}
