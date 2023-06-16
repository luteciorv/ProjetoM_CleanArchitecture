using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Gerar Bearer Jwt utilizando o algoritmo ECDSA com P-256 e SHA-256 (ES256) assimétrico
        /// </summary>
        /// <returns>Jason Web Signature = Jwt assinado digitalmente</returns>
        string GenerateTokenECDSA(User user);

        void EnsureHaveClaim(string token, string claim);

        /// <summary>
        /// Recupera o valor atribuído a claim especificada do token fornecido
        /// </summary>
        string GetClaimValue(string token, string claim);
    }
}
