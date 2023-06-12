using CleanArchitecture.Application.DTOs.Email;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Application.Interfaces.Services;

namespace CleanArchitecture.Infraestructure.Communication.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(CreateEmailDto emailDto)
        {
            string from = _configuration.GetSection("Email:From").Value;
            string host = _configuration.GetSection("Email:Host").Value;
            int port = Convert.ToInt32(_configuration.GetSection("Email:Port").Value);
            string username = _configuration.GetSection("Email:Username").Value;
            string password = _configuration.GetSection("Email:Password").Value;

            var email = new MimeMessage
            {
                Subject = emailDto.Subject,
                Body = new TextPart(TextFormat.Html) { Text = emailDto.Body }
            };

            email.From.Add(MailboxAddress.Parse(from));
            
            foreach(var emailTo in emailDto.EmailsToSend)
                email.To.Add(MailboxAddress.Parse(emailTo));

            using var smtp = new SmtpClient();
         
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);       
        }
    }
}
