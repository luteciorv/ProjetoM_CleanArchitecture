﻿using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Application.Interfaces.Repositories;
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

        [HttpGet("confirm-register/{id:guid}")]
        public async Task ActiveUser(
            [FromServices] IUnitOfWork unitOfWork,
            Guid id,
            CancellationToken cancellationToken
            )
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id, cancellationToken) ?? 
                throw new Exception("Usuário não encontrado");

            user.Email.VerifyEmail();

            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
