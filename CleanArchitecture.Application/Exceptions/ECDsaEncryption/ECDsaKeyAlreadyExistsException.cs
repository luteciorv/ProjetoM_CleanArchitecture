namespace CleanArchitecture.Application.Exceptions.ECDsaEncryption
{
    public class ECDsaKeyAlreadyExistsException : ECDsaException
    {
        public ECDsaKeyAlreadyExistsException(string message) : base(message) { }
    }
}