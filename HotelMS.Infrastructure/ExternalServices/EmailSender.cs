using System.Net;
using System.Net.Mail;
using HotelMS.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace HotelMS.Infrastructure.ExternalServices
{
    public class EmailSender : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = _config["Email:SmtpUser"];
            var password = _config["Email:SmtpPassword"];

            using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(email, password)
            };

            var message = new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(to);

            smtpClient.Send(message);
        }
    }
}