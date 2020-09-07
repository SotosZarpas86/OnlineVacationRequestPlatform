using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using OnlineVacationRequestPlatform.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Services
{
    public class MailService : IMailService
    {
        private readonly SMTPSettings _smtpSettings;

        public MailService(IOptions<SMTPSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string content)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(email));
                message.Subject = subject;
                message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = content };
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                var x = ex;
            }
        }
    }
}
