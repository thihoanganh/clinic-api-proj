using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Services.Interface;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSetting mailSettings;
        public EmailSender(IOptions<EmailSetting> _mailSettings)
        {
            mailSettings = _mailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();

            // use SmtpClient of MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(message);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                smtp.Disconnect(true);
            }



        }
    }
}
