using CleanArchitecture.Application.DTOs.Email;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(CreateEmailDto email);
    }
}
