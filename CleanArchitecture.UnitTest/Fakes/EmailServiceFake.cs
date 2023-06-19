using CleanArchitecture.Application.DTOs.Email;
using CleanArchitecture.Application.Interfaces.Services;

namespace CleanArchitecture.UnitTest.Fakes
{
    internal class EmailServiceFake : IEmailService
    {
        public Task SendEmailAsync(CreateEmailDto email)
        {
            throw new NotImplementedException();
        }
    }
}
