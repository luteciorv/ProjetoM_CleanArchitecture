using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);

        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
