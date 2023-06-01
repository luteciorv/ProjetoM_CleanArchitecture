using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;

namespace CleanArchitecture.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext dataContext) =>
            _dataContext = dataContext;


        private BaseRepository<User> _userRepository;
        public IRepository<User> UserRepository
        {
            get
            {
                _userRepository ??= new BaseRepository<User>(_dataContext);
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
