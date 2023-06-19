using CleanArchitecture.Application.DTOs.ECDsaEncryption;
using CleanArchitecture.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/ecdsa-encryption")]
    [ApiController]
    public class ECDsaEncryptionController : ControllerBase
    {
        /// <summary>
        /// Cria as chaves pública e privada utilizando o algoritmo de criptografia ECDSA com a curva nistP521
        /// </summary>
        /// <returns>Um Dto contendo as chaves pública e privada criadas</returns>
        /// <response code="201">Chaves pública e privada criadas com sucesso</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadECDsaEncryptionDto))]
        [HttpPost("")]
        public ReadECDsaEncryptionDto Post([FromServices] IECDsaEncryptionService ecdsaEncryptionService)
        {
            return ecdsaEncryptionService.CreateKeys();
        }
    }
}
