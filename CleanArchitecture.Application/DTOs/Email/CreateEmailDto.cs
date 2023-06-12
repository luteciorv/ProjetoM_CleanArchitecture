namespace CleanArchitecture.Application.DTOs.Email
{
    public sealed record CreateEmailDto(List<string> EmailsToSend, string Subject, string Body);
}
