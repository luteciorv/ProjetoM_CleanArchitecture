using CleanArchitecture.Application.DTOs.ECDsaEncryption;
using CleanArchitecture.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/ecdsa-encryption")]
    [ApiController]
    public class ECDsaEncryptionController : ControllerBase
    {
        public ECDsaEncryptionController() { }

        [HttpPost]
        public ReadECDsaEncryptionDto Post([FromServices] IECDsaEncryptionService ecdsaEncryptionService)
        {
            return ecdsaEncryptionService.CreateKeys();
        }
    }
}
