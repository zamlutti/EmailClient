using EmailClient.Helpers;

namespace EmailClient.Factories
{
    public interface IEmailFactory
    {
        Email CreateFor(bool isHtml, string subject, string body, params string[] to);
    }
}