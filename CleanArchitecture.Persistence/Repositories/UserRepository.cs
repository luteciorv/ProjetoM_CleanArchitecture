using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Queries;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }


        public async Task<IEnumerable<User>> GetAllActiveAsync(CancellationToken cancellationToken) =>
             await GetAll().Where(UserQueries.GetActives())
            .ToListAsync(cancellationToken);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
            await GetAll().Where(UserQueries.GetByEmail(email))
            .FirstOrDefaultAsync(cancellationToken);

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken) =>
            await GetAll().Where(UserQueries.GetByUsername(username))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
