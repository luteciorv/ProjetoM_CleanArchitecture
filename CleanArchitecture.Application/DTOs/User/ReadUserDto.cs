namespace CleanArchitecture.Application.DTOs.User
{
    public sealed record ReadUserDto(Guid Id, string Username, string Email, bool EmailVerified, int AccessFailedCount, bool IsActive);
}
