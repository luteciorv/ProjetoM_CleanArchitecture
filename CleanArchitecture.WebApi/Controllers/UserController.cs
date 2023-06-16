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
        [HttpGet("")]
        public async Task<IEnumerable<ReadUserDto>> Get(
            [FromServices] IUserService userService, 
            CancellationToken cancellationToken)
        {
            return await userService.GetAllActiveAsync(cancellationToken);
        }
        
        [HttpPost("")]
        public async Task<CreateUserResponse> Post(
            [FromServices] IHandler<CreateUserRequest, CreateUserResponse> handler,
            [FromBody] CreateUserRequest request, 
            CancellationToken cancellationToken)
        {
            return await handler.HandleAsync(request, cancellationToken);          
        }


        [HttpGet("confirm-email/{token}")]
        public async Task<IActionResult> ActiveUser(string token, CancellationToken cancellationToken)
        {
            var request = new ConfirmUserEmailRequest { Token = token };

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsJsonAsync("https://localhost:7108/api/users/confirm-email", request, cancellationToken);

            return Redirect("https://localhost:7108/swagger/index.html");
        }

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
