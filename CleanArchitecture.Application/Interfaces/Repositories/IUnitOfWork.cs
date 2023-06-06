namespace CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
