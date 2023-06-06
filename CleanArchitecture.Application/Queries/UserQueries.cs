using CleanArchitecture.Domain.Entities;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Queries
{
    public static class UserQueries
    {
        public static Expression<Func<User, bool>> GetActives() =>
            user => user.IsActive;

        public static Expression<Func<User, bool>> GetByEmail(string email) =>
            user => user.Email.Address.ToLower() == email.ToLower();

        public static Expression<Func<User, bool>> GetByUsername(string username) =>
                   user => user.Username.ToLower() == username.ToLower();
    }
}
