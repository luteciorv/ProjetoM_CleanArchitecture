using CleanArchitecture.Application.DTOs.User;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ReadUserDto>> GetAllActiveAsync(CancellationToken cancellationToken);
        Task<bool> CheckEmailRegisteredAsync(string email, CancellationToken cancellationToken);
        Task<bool> CheckUsernameRegisteredAsync(string username, CancellationToken cancellationToken);
        Task<byte[]> GeneratePasswordHashAsync(string password, string salt);
    }
}
