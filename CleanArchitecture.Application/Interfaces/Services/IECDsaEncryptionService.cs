using CleanArchitecture.Application.DTOs.ECDsaEncryption;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IECDsaEncryptionService
    {
        /// <summary>
        /// Cria as chaves pública e privada utilizando o algoritmo de criptografia ECDsa
        /// </summary>
        ReadECDsaEncryptionDto CreateKeys();
        ECDsaSecurityKey GetPublicKey();
        ECDsaSecurityKey GetPrivateKey();
    }
}
