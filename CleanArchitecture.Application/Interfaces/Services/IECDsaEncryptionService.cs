using CleanArchitecture.Application.DTOs.ECDsaEncryption;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IECDsaEncryptionService
    {
        ReadECDsaEncryptionDto CreateKeys();
        ECDsaSecurityKey GetPublicKey();
        ECDsaSecurityKey GetPrivateKey();
    }
}
