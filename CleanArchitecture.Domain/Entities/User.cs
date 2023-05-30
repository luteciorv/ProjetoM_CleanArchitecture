using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string email) =>
        (Name, Email) = (name, email);

        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
