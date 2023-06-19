using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.UnitTest.Fakes
{
    internal class TokerServiceFake : ITokenService
    {
        public void EnsureHaveClaim(string token, string claim)
        {
            throw new NotImplementedException();
        }

        public string GenerateTokenECDSA(User user)
        {
            throw new NotImplementedException();
        }

        public string GetClaimValue(string token, string claim)
        {
            throw new NotImplementedException();
        }
    }
}
