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
        private static readonly string FromAccountName =
            ConfigurationManager.AppSettings["from-account-name"];
        private static readonly string FromAccountPassword = 
            ConfigurationManager.AppSettings["from-account-password"];
        private static readonly string SmtpServer = 
            ConfigurationManager.AppSettings["smtp-server"];
        private const int SmtpPort = 587;

        public void Send(Email email)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email.From ?? FromAccountName),
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
                Credentials = new NetworkCredential(FromAccountName, FromAccountPassword)
            };

            smtp.Send(mailMessage);
        }
    }
}
