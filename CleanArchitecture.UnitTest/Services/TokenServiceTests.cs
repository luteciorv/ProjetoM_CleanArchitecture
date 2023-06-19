using CleanArchitecture.Application.Exceptions.Claims;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Infraestructure.Security.Services;
using Moq;

namespace CleanArchitecture.UnitTest.Services
{
    [TestClass]
    public class TokenServiceTests
    {
        /*Payload{
            "Id": "8d2857a6-d524-4171-9558-cbfbf06445b6",
            "E-mail": "Teste11@localhost.com",
            "Username": "Teste11",
            "aud": "ProjetoM.WebApi",
            "exp": 1687180974,
            "iss": "https://localhost:7108",
            "iat": 1687179174,
            "nbf": 1687179174
            }
         */
        private const string TOKEN = "eyJhbGciOiJFUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjhkMjg1N2E2LWQ1MjQtNDE3MS05NTU4LWNiZmJmMDY0NDViNiIsIkUtbWFpbCI6IlRlc3RlMTFAbG9jYWxob3N0LmNvbSIsIlVzZXJuYW1lIjoiVGVzdGUxMSIsImF1ZCI6IlByb2pldG9NLldlYkFwaSIsImV4cCI6MTY4NzE4MDk3NCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzEwOCIsImlhdCI6MTY4NzE3OTE3NCwibmJmIjoxNjg3MTc5MTc0fQ.AFefKMYXHGGF_u0qm7YblNPkbiSh0vGREDTDVhKy3yF7Yqcn_qYTMwLAbLvhEtKHsR2NTZBr0ZTUN_V3wJqlhT0NAZree6TWX8ERal8mmjDgfn51vPwyPO51THpXd3VlHKyNRNq1AFGhFNwm-Wc1euj61PFe2HfGEobzjI2V027IQqKq";

        private readonly ITokenService _tokenService;
        public TokenServiceTests()
        {
            var ecdsaEncryptionMock = new Mock<IECDsaEncryptionService>();
            _tokenService = new TokenService(ecdsaEncryptionMock.Object);
        }

        [TestMethod]
        [DataRow("id")]
        [DataRow("endereço de e-mail")]
        [DataRow("usuario")]
        public void Dado_um_token_valido_sem_determinada_claim_deve_levantar_excecao_ClaimException(string claim)
        {
           Assert.ThrowsException<ClaimException>(() => _tokenService.EnsureHaveClaim(TOKEN, claim));
        }

        [TestMethod]
        [DataRow("Id")]
        [DataRow("E-mail")]
        [DataRow("Username")]
        public void Dado_um_token_valido_com_determinada_claim_existente_nenhuma_excecao_deve_ser_levantada(string claim)
        {
            _tokenService.EnsureHaveClaim(TOKEN, claim);
        }

        [TestMethod]
        [DataRow("Id", "8d2857a6-d524-4171-9558-cbfbf06445b6")]
        [DataRow("E-mail", "Teste11@localhost.com")]
        [DataRow("Username", "Teste11")]
        public void Dado_um_token_valido_com_determinada_claim_existente_seu_valor_deve_ser_retornado(string claim, string claimValue)
        {
            var claimResult = _tokenService.GetClaimValue(TOKEN, claim);
            Assert.AreEqual(claimResult, claimValue);
        }
    }
}
