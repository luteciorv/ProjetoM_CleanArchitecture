using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities
{
    public sealed class Profile : BaseEntity
    {
        private Profile() { }

        public Profile(Name name, int age, EGender gender, Document document, User user,  
            string? occupation = null, string? linkedin = null, string? github = null, string? instagram = null)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Document = document;

            Occupation = occupation;
            Linkedin = linkedin;
            Github = github;
            Instagram = instagram;

            UserId = user.Id;
            User = user;
        }

        public Name Name { get; private set; }
        public int Age { get; private set; }
        public EGender Gender { get; private set; }
        public Document Document { get; private set; }

        public string? Occupation { get; set; }
        public string? Linkedin { get; private set; }
        public string? Github { get; private set; }
        public string? Instagram { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }
    }
}
