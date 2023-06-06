using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Persistence.Context;

namespace CleanArchitecture.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext dataContext) =>
            _dataContext = dataContext;


        private UserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_dataContext);
                return _userRepository;
            }
        }


        public async Task SaveAsync(CancellationToken cancellationToken) =>
            await _dataContext.SaveChangesAsync(cancellationToken);


        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (_disposed) if (disposing) _dataContext.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
