using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        { }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
            await Context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);    
    }
}
