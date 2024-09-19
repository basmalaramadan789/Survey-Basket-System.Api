using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using SurveyBasket.Api.Settings;


namespace SurveyBasket.Api.Services
{
    public class EmailService(IOptions<MailSettings> mailSettings) : IEmailSender
    {
        private readonly MailSettings _mailSettings = mailSettings.Value;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = subject
            };
            message.To.Add(MailboxAddress.Parse(email));

            var builder = new BodyBuilder
            {
                HtmlBody = htmlMessage,

            };
            message.Body = builder.ToMessageBody();

            using var stmp = new SmtpClient();
            stmp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

            stmp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await stmp.SendAsync(message);
            stmp.Disconnect(true);
        }
    }
}
