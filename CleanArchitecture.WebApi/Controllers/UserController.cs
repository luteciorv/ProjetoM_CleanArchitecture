using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Resources.UserResources.ConfirmUserEmail;
using CleanArchitecture.Application.Resources.UserResources.CreateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Recupera todos os usuários ativos
        /// </summary>
        /// <returns>Um IEnumerable de usuários ativos</returns>
        /// <response code="200">Usuários ativos recuperados com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReadUserDto>))]
        [HttpGet("")]
        public async Task<IEnumerable<ReadUserDto>> Get(
            [FromServices] IUserService userService, 
            CancellationToken cancellationToken)
        {
            return await userService.GetAllActiveAsync(cancellationToken);
        }

        /// <summary>Cria um novo usuário</summary>
        /// <param name="request">Informações do novo usuário</param>
        /// <param name="handler"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Novo usuário criado com sucesso</returns>
        /// <response code="201">Usuário criado com sucesso</response>
        /// <response code="400">Informações inválidas no corpo da requisição</response>
        /// <response code="500">Erro não mapeado no servidor</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json"), Produces("application/json")]
        [HttpPost("")]
        public async Task<CreateUserResponse> Post(
            [FromServices] IHandler<CreateUserRequest, CreateUserResponse> handler,
            [FromBody] CreateUserRequest request, 
            CancellationToken cancellationToken)
        {
            return await handler.HandleAsync(request, cancellationToken);          
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("confirm-email/{token}")]
        public async Task<IActionResult> ActiveUser(string token, CancellationToken cancellationToken)
        {
            var request = new ConfirmUserEmailRequest { Token = token };

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsJsonAsync("https://localhost:7108/api/users/confirm-email", request, cancellationToken);

            return Redirect("https://localhost:7108/swagger/index.html");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(AuthenticationSchemes = "JwtECDsa")]
        [HttpPut("confirm-email")]
        public async Task<ConfirmUserEmailResponse> ActiveUser(
            [FromServices] IHandler<ConfirmUserEmailRequest, ConfirmUserEmailResponse> handler,
            [FromBody] ConfirmUserEmailRequest request,
            CancellationToken cancellationToken)
        {
            return await handler.HandleAsync(request, cancellationToken);
        }
    }
}
