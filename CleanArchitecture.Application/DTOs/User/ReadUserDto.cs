namespace CleanArchitecture.Application.DTOs.User
{
    public sealed class ReadUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsActive { get; set; }
    }

}
