namespace CleanArchitecture.Application.DTOs.ECDsaEncryption
{
    public sealed record ReadECDsaEncryptionDto(bool IsSuccess, string Message, string PublicKey, string PrivateKey);
}
