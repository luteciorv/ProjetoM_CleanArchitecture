using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Resources.UserResources.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IEnumerable<ReadUserDto>> GetAsync(
            [FromServices] IUserService userService, 
            CancellationToken cancellationToken)
        {
            var response = await userService.GetAllAsync(cancellationToken);
            return response;
        }

        [HttpPost("")]
        public async Task<CreateUserResponse> PostAsync(
            [FromServices] IHandler<CreateUserRequest, CreateUserResponse> handler,
            CreateUserRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await handler.HandleAsync(request, cancellationToken);          
            return response;
        }      
    }
}
