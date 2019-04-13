using System;
using MimeKit;
using MailKit.Net.Smtp;

namespace Authentication2.Mail
{
    public class Mailer : IMailer
    {
        public void SendMail(string subject, string email, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("oshipemailer@gmail.com"));
            message.To.Add(new MailboxAddress(email));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = "Order accepted"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                client.Authenticate("oshipemailer@gmail.com", "Gavin123!");
                client.Send(message);
                client.Disconnect(false);
            }
        }
    }
}
