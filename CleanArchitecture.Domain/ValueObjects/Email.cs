namespace CleanArchitecture.Domain.ValueObjects
{
    public record Email
    {
        public Email(string address)
        {
            Address = address;
            Verified = false;      
        }

        public string Address { get; private set; }
        public bool Verified { get; private set; }

        public void ConfirmEmail() => Verified = true;
    }
}
