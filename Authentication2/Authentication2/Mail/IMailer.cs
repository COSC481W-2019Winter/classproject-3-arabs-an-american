using System;
namespace Authentication2.Mail
{
    public interface IMailer
    {
        void SendMail(string subject, string email, string message);
    }
}
