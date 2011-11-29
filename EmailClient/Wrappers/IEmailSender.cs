using EmailClient.Helpers;

namespace EmailClient.Wrappers
{
    public interface IEmailSender
    {
        void Send(Email email);
    }
}