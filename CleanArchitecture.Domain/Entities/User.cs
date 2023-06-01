using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
            DateCreated = DateTime.UtcNow;
            DateUpdated = DateTime.UtcNow;
        }
        

        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
