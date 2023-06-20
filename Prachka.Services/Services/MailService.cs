
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.Services
{
    public class MailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var newMessage = new MimeMessage();
            newMessage.From.Add(MailboxAddress.Parse("prachka.prachka@yandex.ru"));
            newMessage.To.Add(MailboxAddress.Parse(email));
            newMessage.Subject = subject;
            newMessage.Body = new TextPart(TextFormat.Html) { Text = message };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.yandex.ru", 465, SecureSocketOptions.Auto);
            smtp.Authenticate("prachka.prachka@yandex.ru", "szshmpjyjyprmses");
            smtp.Send(newMessage);
            smtp.Disconnect(true);
        }
    }
}
