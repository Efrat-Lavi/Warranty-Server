
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Models;
using System;
using System.Threading.Tasks;

namespace Warranty.Service
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(EmailRequest request)
        {
            var smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER");
            var port = Environment.GetEnvironmentVariable("SMTP_PORT");
            var emailUser = Environment.GetEnvironmentVariable("GOOGLE_USER_EMAIL");
            var emailPassword = Environment.GetEnvironmentVariable("GOOGLE_USER_PASSWORD");

            Console.WriteLine(emailUser);

            if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(port) ||
                string.IsNullOrEmpty(emailUser) || string.IsNullOrEmpty(emailPassword))
            {
                Console.WriteLine("❌ Missing environment variables!");
                return false;
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Keep It", emailUser));
            emailMessage.To.Add(new MailboxAddress(request.To, request.To));
            emailMessage.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = request.IsHtml ? request.Body : null,
                TextBody = request.IsHtml ? null : request.Body
            };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(smtpServer, int.Parse(port), SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(emailUser, emailPassword);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                    Console.WriteLine("✅ Email sent successfully!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Email sending failed: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
