using CleanArchitecture.Domain.Entities;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Queries
{
    public static class UserQueries
    {
        public static Expression<Func<User, bool>> GetByEmail(string email) =>
            user => user.Email.ToLower() == email.ToLower();
    }
}
