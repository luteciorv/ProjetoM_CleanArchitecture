using MediatR;

namespace CleanArchitecture.Application.Resources.UserResources.CreateUser
{
    public sealed record CreateUserRequest(string Email, string Name) : IRequest<CreateUserResponse>;
}
