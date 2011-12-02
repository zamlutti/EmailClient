using System.Collections.Generic;
using System.Text;
using EmailClient.Builders;
using EmailClient.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace EmailClient.UnitTests.Factories
{
    [TestClass]
    public class EmailFactoryTest
    {
        private EmailFactory _emailFactory;
        private Mock<IEmailContentBuilder> _contentBuilderMock;
        private IEnumerable<KeyValuePair<string, string>> _valuesToReplaceOnBodyTemplate;
        private const string Subject = "subject";
        private const string Body = "body";
        private const string Recipients = "an-email";
        private const bool IsHtml = false;

        [TestInitialize]
        public void SetUp()
        {
            _valuesToReplaceOnBodyTemplate = new Dictionary<string, string>
                                                 {
                                                     {"first-key","first-value"}
                                                 };
            _contentBuilderMock = new Mock<IEmailContentBuilder>();
            _emailFactory = new EmailFactory(_contentBuilderMock.Object);
        }

        [TestMethod]
        public void ShouldCreateAnEmail()
        {
            var email = _emailFactory.CreateFor(IsHtml, Subject, Body, Recipients);

            email.IsHtml.Should().Be.EqualTo(IsHtml);
            email.From.Should().Be.Null();
            email.Subject.Should().Be.EqualTo(Subject);
            email.Body.Should().Be.EqualTo(Body);
            email.To.Should().Contain(Recipients);
            email.To.Should().Have.Count.EqualTo(1);
            email.Cc.Should().Be.Empty();
            email.Bcc.Should().Be.Empty();
            email.Encoding.Should().Be.EqualTo(Encoding.UTF8);
        }

        [TestMethod]
        public void ShouldCreateAnEmailFromTemplate()
        {
            GivenTheSubjectCanBeBuild()
                .AndTheBodyCanBeBuild();

            var email = _emailFactory
                .CreateFromTemplateFor(IsHtml, Subject, Body, _valuesToReplaceOnBodyTemplate, Recipients);

            email.IsHtml.Should().Be.EqualTo(IsHtml);
            email.From.Should().Be.Null();
            email.Subject.Should().Be.EqualTo(Subject);
            email.Body.Should().Be.EqualTo(Body);
            email.To.Should().Contain(Recipients);
            email.To.Should().Have.Count.EqualTo(1);
            email.Cc.Should().Be.Empty();
            email.Bcc.Should().Be.Empty();
            email.Encoding.Should().Be.EqualTo(Encoding.UTF8);
        }

        [TestMethod]
        public void ShouldBuildSubjectWhenCreatingAnEmailFromTemplate()
        {
            GivenTheSubjectCanBeBuild()
                .AndTheBodyCanBeBuild();

            _emailFactory
                .CreateFromTemplateFor(IsHtml, Subject, Body, _valuesToReplaceOnBodyTemplate, Recipients);
            _contentBuilderMock.Verify(it => it.BuildSubjectFor(@"templates-path\subject"), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildBodyWhenCreatingAnEmailFromTemplate()
        {
            GivenTheSubjectCanBeBuild()
                .AndTheBodyCanBeBuild();

            _emailFactory
                .CreateFromTemplateFor(IsHtml, Subject, Body, _valuesToReplaceOnBodyTemplate, Recipients);

            _contentBuilderMock.Verify(it => it.BuildBodyFor(_valuesToReplaceOnBodyTemplate, @"templates-path\body"),
                                       Times.Once());
        }


        private EmailFactoryTest GivenTheSubjectCanBeBuild()
        {
            _contentBuilderMock
                .Setup(it => it.BuildSubjectFor(It.IsAny<string>()))
                .Returns(Subject);
            return this;
        }

        private void AndTheBodyCanBeBuild()
        {
            _contentBuilderMock
                .Setup(it => it.BuildBodyFor(It.IsAny<IDictionary<string, string>>(),
                                             It.IsAny<string>()))
                .Returns(Body);
        }
    }
}