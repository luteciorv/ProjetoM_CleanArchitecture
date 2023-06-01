using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
