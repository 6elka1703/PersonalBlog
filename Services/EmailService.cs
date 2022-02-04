using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;

namespace PersonalBlog.Services
{
    public class EmailService
    {
        public IConfiguration config { get; }

        public EmailService(IConfiguration configuration)
        {
            config = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта Personal Blog", config["EmailSettings:email"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(config["EmailSettings:smtp"], Convert.ToInt32(config["EmailSettings:port"]), true);
                await client.AuthenticateAsync(config["EmailSettings:email"], config["EmailSettings:password"]);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}

