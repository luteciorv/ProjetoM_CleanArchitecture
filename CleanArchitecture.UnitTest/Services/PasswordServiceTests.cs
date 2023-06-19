using CleanArchitecture.Application.Exceptions.PasswordEntropy;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Infraestructure.Security.Services;
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
        [DataRow("senhafraca")]
        [DataRow("SenhaMedia")]
        [DataRow("S3Nh@CoMpL3x@")]
        public async Task Dado_uma_senha_deve_hashear_ela(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var salt = await _passwordService.GenerateSaltAsync();
            var passwordHash = await _passwordService.CreateHashAsync(passwordBytes, salt);

            var passwordToCompareBytes = Encoding.UTF8.GetBytes(password);

            Assert.IsTrue(await _passwordService.VerifyPasswordAsync(passwordToCompareBytes, salt, passwordHash));
        }

        [TestMethod]
        [DataRow("070800")]
        public async Task Dado_uma_senha_deve_possuir_uma_entropia_ruim(string password)
        {
            await Assert.ThrowsExceptionAsync<PoorPasswordEntropyException>(async () => await _passwordService.EnsureEntropyIsValid(password));
        }

        [TestMethod]
        [DataRow("TeStEdOs")]
        public async Task Dado_uma_senha_deve_possuir_uma_entropia_fraca(string password)
        {
            await Assert.ThrowsExceptionAsync<WeakPasswordEntropyException>(async () => await _passwordService.EnsureEntropyIsValid(password));
        }
    }
}
