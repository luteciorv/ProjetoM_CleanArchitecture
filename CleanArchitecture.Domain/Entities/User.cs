using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        private User() { }

        public User(string username, Email email, string password)
        {
            Username = username;
            Email = email;
            Password = password;

            AccessFailedCount = 0;
            IsActive = true;
        }

        public string Username { get; private set; }
        public Email Email { get; private set; }
        public string Password { get; private set; }
        public int AccessFailedCount { get; private set; }
        public bool IsActive { get; private set; }

        public Profile? Profile { get; private set; }


        public void FailedToLogin() => AccessFailedCount++;
        public void AssignProfile(Profile profile) => Profile = profile;
        public void Delete()
        {
            DateDeleted = DateTime.UtcNow;
            IsActive = false;
        }
    }
}
