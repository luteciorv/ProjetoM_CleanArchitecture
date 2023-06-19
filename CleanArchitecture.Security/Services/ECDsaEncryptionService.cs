using CleanArchitecture.Application.DTOs.ECDsaEncryption;
using CleanArchitecture.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace CleanArchitecture.Infraestructure.Security.Services
{
    public class ECDsaEncryptionService : IECDsaEncryptionService
    {
        private readonly IConfiguration _configuration;

        public ECDsaEncryptionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ReadECDsaEncryptionDto CreateKeys()
        {         
            var securityKeys = new ECDsaSecurityKey(ECDsa.Create(ECCurve.NamedCurves.nistP521))
            {
                KeyId = new Guid().ToString()
            };

            var publicKey = Convert.ToBase64String(securityKeys.ECDsa.ExportSubjectPublicKeyInfo()); 
            var privateKey = Convert.ToBase64String(securityKeys.ECDsa.ExportPkcs8PrivateKey());

            return new ReadECDsaEncryptionDto(true, 
                "As chaves de criptografia utilizando o algoritmo ECDsa foram criadas e armazenadas com criptografia RSA no banco de dados com sucesso.",
                publicKey,
                privateKey
            );
        }   

        public ECDsaSecurityKey GetPublicKey()
        {
            var publicKey = Convert.FromBase64String(_configuration["ECDsaEncryption:PublicKey"]);
            var ecdsaSecurityKeys = new ECDsaSecurityKey(ECDsa.Create());
            ecdsaSecurityKeys.ECDsa.ImportSubjectPublicKeyInfo(publicKey, out _);

            return ecdsaSecurityKeys;
        }

        public ECDsaSecurityKey GetPrivateKey()
        {
            var privateKey = Convert.FromBase64String(_configuration["ECDsaEncryption:PrivateKey"]);
            var ecdsaSecurityKeys = new ECDsaSecurityKey(ECDsa.Create());
            ecdsaSecurityKeys.ECDsa.ImportPkcs8PrivateKey(privateKey, out _);

            return ecdsaSecurityKeys;
        }
    }
}
