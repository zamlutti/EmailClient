using System.Configuration;
using System.Net;
using System.Net.Mail;
using EmailClient.Helpers;
using MACSkeptic.Commons.Extensions;
using SimplInject;

namespace EmailClient.Wrappers
{
    [SimplInject]
    public class EmailSender : IEmailSender
    {
        private static readonly string FromAccountEmail =
            ConfigurationManager.AppSettings["EmailClient.Account"];
        private static readonly string FromAccountPassword = 
            ConfigurationManager.AppSettings["EmailClient.Password"];
        private static readonly string SmtpServer = 
            ConfigurationManager.AppSettings["EmailClient.SMTP"];
        private const int SmtpPort = 587;

        public void Send(Email email)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email.From ?? FromAccountEmail),
                BodyEncoding = email.Encoding,
                SubjectEncoding = email.Encoding,
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = email.IsHtml
            };

            email.To.Each(to => mailMessage.To.Add(new MailAddress(to)));
            email.Cc.Each(cc => mailMessage.CC.Add(new MailAddress(cc)));
            email.Bcc.Each(bcc => mailMessage.Bcc.Add(new MailAddress(bcc)));

            var smtp = new SmtpClient(SmtpServer, SmtpPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromAccountEmail, FromAccountPassword)
            };

            smtp.Send(mailMessage);
        }
    }
}
