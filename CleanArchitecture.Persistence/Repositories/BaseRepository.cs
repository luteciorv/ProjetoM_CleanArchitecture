using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataContext Context;
        public BaseRepository(DataContext context) =>
            Context = context;

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken) =>
            await Context.Set<TEntity>().ToListAsync(cancellationToken);
        public async Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken) =>
            await Context.Set<TEntity>().FindAsync(id, cancellationToken);

        public void Create(TEntity entity) =>
            Context.Add(entity);
        public void Delete(TEntity entity)
        {
            entity.DateDeleted = DateTime.UtcNow;
            Context.Update(entity);
        }
        public void Update(TEntity entity) =>
            Context.Update(entity);
    }
}
