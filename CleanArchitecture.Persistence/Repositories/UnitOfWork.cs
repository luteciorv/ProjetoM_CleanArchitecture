using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Persistence.Context;

namespace CleanArchitecture.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext dataContext) =>
            _dataContext = dataContext;

        public async Task SaveAsync(CancellationToken cancellationToken) =>
            await _dataContext.SaveChangesAsync(cancellationToken);
    }
}
