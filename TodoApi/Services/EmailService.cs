using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace TodoApi.Services
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPass { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }

    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body, bool isHtml = false);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string subject, string body, bool isHtml = false)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_settings.From));
            message.To.Add(MailboxAddress.Parse(_settings.To));
            message.Subject = subject;
            message.Body = new TextPart(isHtml ? "html" : "plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, false);
            await client.AuthenticateAsync(_settings.SmtpUser, _settings.SmtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}