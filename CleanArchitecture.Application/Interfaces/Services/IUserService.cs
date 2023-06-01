using CleanArchitecture.Application.DTOs.User;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ReadUserDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> CheckEmailRegisteredAsync(string email, CancellationToken cancellationToken);
    }
}
