using CleanArchitecture.Application.DTOs.ECDsaEncryption;
using CleanArchitecture.Application.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.UnitTest.Fakes
{
    internal class ECDsaEncryptionServiceFake : IECDsaEncryptionService
    {
        public ReadECDsaEncryptionDto CreateKeys()
        {
            throw new NotImplementedException();
        }

        public ECDsaSecurityKey GetPrivateKey()
        {
            throw new NotImplementedException();
        }

        public ECDsaSecurityKey GetPublicKey()
        {
            throw new NotImplementedException();
        }
    }
}
