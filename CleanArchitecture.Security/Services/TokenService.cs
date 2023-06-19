using CleanArchitecture.Application.Exceptions.Claims;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Statics;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArchitecture.Infraestructure.Security.Services
{
    public class TokenService : ITokenService
    {
        private readonly IECDsaEncryptionService _ecdsaEncryptionService;

        public TokenService(IECDsaEncryptionService ecdsaEncryptionService)
        {
            _ecdsaEncryptionService = ecdsaEncryptionService;
        }

        public string GenerateTokenECDSA(User user)
        {
            var keys = _ecdsaEncryptionService.GetPrivateKey();
            var jwt = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:7108",
                Audience = "ProjetoM.WebApi",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(0.5),
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(UserClaims.ID, user.Id.ToString()),
                    new Claim(UserClaims.EMAIL, user.Email.Address, ClaimValueTypes.Email),
                    new Claim(UserClaims.USERNAME, user.Username)
                }),
                SigningCredentials = new SigningCredentials(keys, SecurityAlgorithms.EcdsaSha512)
            };

            return new JsonWebTokenHandler().CreateToken(jwt);
        }

        public void EnsureHaveClaim(string token, string claim)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var claimFound = jwt.Claims.FirstOrDefault(c => c.Type == claim) ??
                throw new ClaimException($"O Token informado não possui a claim {claim}.");
        }

        public string GetClaimValue(string token, string claim)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var teste = jwt.Claims.FirstOrDefault(c => c.Type == claim);
            return jwt.Claims.FirstOrDefault(c => c.Type == claim).Value;
        }
    }
}