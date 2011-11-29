using System.Text;
using EmailClient.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace EmailClient.UnitTests.Factories
{
    [TestClass]
    public class EmailFactoryTest
    {
        [TestMethod]
        public void ShouldCreateAnEmail()
        {
            const string subject = "subject";
            const string body = "body";
            const string aEmail = "a-email";
            const bool isHtml = false;
            
            var email = new EmailFactory().CreateFor(isHtml, subject, body, aEmail);
            email.IsHtml.Should().Be.EqualTo(isHtml);
            email.From.Should().Be.Null();
            email.Subject.Should().Be.EqualTo(subject);
            email.Body.Should().Be.EqualTo(body);
            email.To.Should().Contain(aEmail);
            email.To.Should().Have.Count.EqualTo(1);
            email.Cc.Should().Be.Empty();
            email.Bcc.Should().Be.Empty();
            email.Encoding.Should().Be.EqualTo(Encoding.UTF8);
        }
    }
}
