using System;
using System.Text;
using EmailClient.Builders;
using EmailClient.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace EmailClient.UnitTests.Builders
{
    [TestClass]
    public class ContentTemplateBuilderTest
    {
        private Mock<IReadFileWrapper> _wrapperMock;
        private ContentTemplateBuilder _builder;
        private const string TemplateName = "template-name";
        private const string EmailContentTemplate = "{0}-{1}";
        private const string FirstKey = "first-key";
        private const string SecondKey = "second-key";
        private const string FirstValue = "first-value";
        private const string SecondValue = "second-value";
        private string _emailTemplateRetrieved;

        [TestInitialize]
        public void SetUp()
        {
            _emailTemplateRetrieved = String.Format(EmailContentTemplate,
                                                    "{" + FirstKey + "}",
                                                    "{" + SecondKey + "}");

            _wrapperMock = new Mock<IReadFileWrapper>();
            _wrapperMock
                .Setup(it => it.Read(It.IsAny<string>(), It.IsAny<Encoding>()))
                .Returns(_emailTemplateRetrieved);

            _builder = new ContentTemplateBuilder(_wrapperMock.Object);
        }

        [TestMethod]
        public void ShouldBuildContentBasedOnTemplate()
        {
            _builder
                .UsingTemplate(TemplateName)
                .ReplaceKeyWith(FirstKey, FirstValue)
                .ReplaceKeyWith(SecondKey, SecondValue)
                .Build().Should().Be.EqualTo(String.Format("{0}-{1}", FirstValue, SecondValue));
        }

        [TestMethod]
        public void ShouldReadFileWhenBuildingContent()
        {
            _builder
                .UsingTemplate(TemplateName)
                .ReplaceKeyWith(FirstKey, FirstValue)
                .ReplaceKeyWith(SecondKey, SecondValue)
                .Build();

            _wrapperMock.Verify(it => it.Read(TemplateName, Encoding.UTF8), Times.Once());
        }
    }
}
