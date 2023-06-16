namespace CleanArchitecture.Application.Exceptions.ECDsaEncryption
{
    public class ECDsaException : Exception
    {
        public ECDsaException(string message) : base(message) { }
    }
}