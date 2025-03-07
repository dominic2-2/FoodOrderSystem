using FoodOrderSystem.Services.Implements;
using System.Net.Mail;
using System.Net;
using System.Net.WebSockets;

namespace FoodOrderSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
                var smtpClient = new SmtpClient(_configuration["Email:SmtpServer"])
                {
                    Port = int.Parse(_configuration["Email:SmtpPort"]),
                    Credentials = new NetworkCredential(_configuration["Email:Username"], smtpPass),
                    EnableSsl = bool.Parse(_configuration["Email:EnableSSL"])
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["Email:Username"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
