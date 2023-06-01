using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _entity;

        public BaseRepository(DataContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }
            

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
            await _entity.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _entity.FindAsync(new object?[] { id }, cancellationToken);

        public TEntity? GetBy(Expression<Func<TEntity, bool>> predicate) =>
            _entity.AsQueryable().Where(predicate).FirstOrDefault();

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) =>
            await _entity.AsQueryable().AnyAsync(predicate, cancellationToken);

        public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken) => await _context.AddAsync(entity, cancellationToken);
        public void Delete(TEntity entity) => _context.Remove(entity);
        public void Update(TEntity entity) => _context.Update(entity);
    }
}
