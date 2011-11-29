using System.Text;
using EmailClient.Helpers;
using SimplInject;

namespace EmailClient.Factories
{
    [SimplInject]
    public class EmailFactory : IEmailFactory
    {
        public Email CreateFor(bool isHtml, string subject, string body, params string[] to)
        {
            return new Email
            {
                Subject = subject,
                Body = body,
                To = to,
                Encoding = Encoding.UTF8,
                IsHtml = isHtml
            };
        }
    }
}
