using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Infraestructure.Security.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UnitTest.Services
{
    [TestClass]
    public class TokenServiceTests
    {
        private readonly ITokenService tokenService;
        public TokenServiceTests()
        {
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["Jwt:KeysPath"]).Returns("C:\\Codigos\\ProjetoM_CleanArchitecture\\CleanArchitecture.Security\\Keys\\keys-edcsa.key");
            //tokenService = new TokenService(configurationMock.Object);
        }

        [TestMethod]
        public void Gerar_Token_valido()
        {
            using RSA rsa = RSA.Create();
            Console.WriteLine("Algoritmo RSA");
            Console.WriteLine($"Private key--{Environment.NewLine}{Convert.ToBase64String(rsa.ExportRSAPrivateKey())}{Environment.NewLine}");
            Console.WriteLine($"Public key--{Environment.NewLine}{Convert.ToBase64String(rsa.ExportRSAPublicKey())}");
            
            Console.WriteLine("\n");
            Assert.Fail();
        }

        [TestMethod]
        public async Task Gerar_Token_valido_ECDSA()
        {
            //var token = await tokenService.GenerateTokenAsync();

            Assert.Fail();
        }
    }
}
