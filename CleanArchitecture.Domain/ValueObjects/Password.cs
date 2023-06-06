namespace CleanArchitecture.Domain.ValueObjects
{
    public record Password
    {
        public Password(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public string Hash { get; private set; }
        public string Salt { get; private set; }
    }
}
