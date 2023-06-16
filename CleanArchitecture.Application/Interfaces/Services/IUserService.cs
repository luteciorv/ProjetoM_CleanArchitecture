using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Application.Resources.UserResources.CreateUser;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ReadUserDto>> GetAllActiveAsync(CancellationToken cancellationToken);
        Task EnsureEmailNotRegisteredAsync(string email, CancellationToken cancellationToken);
        Task EnsureUsernameNotRegisteredAsync(string username, CancellationToken cancellationToken);

        Task<User> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
        Task<Password> CreatePasswordAsync(string password);

        Task ConfirmEmailAsync(string email, CancellationToken cancellationToken);
    }
}
