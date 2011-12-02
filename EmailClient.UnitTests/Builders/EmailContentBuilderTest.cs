using System.Collections.Generic;
using System.Linq;
using EmailClient.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace EmailClient.UnitTests.Builders
{
    [TestClass]
    public class EmailContentBuilderTest
    {
        private EmailContentBuilder _contentBuilder;
        private Mock<IContentTemplateBuilder> _contentTemplateBuilderMock;
        private const string Subject = "subject";
        private const string Body = "body";
        private const string SubjectTemplatePath = "subject-template-path";
        private const string BodyTemplatePath = "body-template-path";
        private Dictionary<string, string> _values;

        [TestInitialize]
        public void SetUp()
        {
            _contentTemplateBuilderMock = new Mock<IContentTemplateBuilder>();
            _contentBuilder = new EmailContentBuilder(_contentTemplateBuilderMock.Object);
            _values = new Dictionary<string, string>
                          {
                              {"key", "value"}
                          };
        }

        [TestMethod]
        public void ShouldBuildSubject()
        {
            GivenTheSubjectCanBeBuild();

            _contentBuilder.BuildSubjectFor(SubjectTemplatePath).Should().Be.EqualTo(Subject);
        }

        [TestMethod]
        public void ShouldUseSubjectTemplateWhenBuildingSubject()
        {
            GivenTheSubjectCanBeBuild();

            _contentBuilder.BuildSubjectFor(SubjectTemplatePath);
            _contentTemplateBuilderMock.Verify(it=>it.UsingTemplate(SubjectTemplatePath), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSubjectTemplateWhenBuildingSubject()
        {
            GivenTheSubjectCanBeBuild();

            _contentBuilder.BuildSubjectFor(SubjectTemplatePath);
            _contentTemplateBuilderMock.Verify(it=>it.Build(), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildBody()
        {
            GivenTheBodyCanBeBuild();

            _contentBuilder.BuildBodyFor(_values, BodyTemplatePath).Should().Be.EqualTo(Body);
        }

        [TestMethod]
        public void ShouldUseBodyTemplateWhenBuildingBody()
        {
            GivenTheBodyCanBeBuild();

            _contentBuilder.BuildBodyFor(_values, BodyTemplatePath);
            _contentTemplateBuilderMock.Verify(it=>it.UsingTemplate(BodyTemplatePath), Times.Once());
        }

        [TestMethod]
        public void ShouldReplaceValuesWhenBuildingBody()
        {
            GivenTheBodyCanBeBuild();

            _contentBuilder.BuildBodyFor(_values, BodyTemplatePath);
            _contentTemplateBuilderMock
                .Verify(it=>it.ReplaceKeyWith(_values.First().Key,_values.First().Value), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildBodyTemplateWhenBuildingBody()
        {
            GivenTheBodyCanBeBuild();

            _contentBuilder.BuildBodyFor(_values, BodyTemplatePath);
            _contentTemplateBuilderMock.Verify(it=>it.Build(), Times.Once());
        }

        private void GivenTheSubjectCanBeBuild()
        {
            _contentTemplateBuilderMock
                .Setup(it => it.UsingTemplate(It.IsAny<string>()))
                .Returns(_contentTemplateBuilderMock.Object);

            _contentTemplateBuilderMock
                .Setup(it => it.Build())
                .Returns(Subject);
        }

        private void GivenTheBodyCanBeBuild()
        {
            _contentTemplateBuilderMock
                .Setup(it => it.UsingTemplate(It.IsAny<string>()))
                .Returns(_contentTemplateBuilderMock.Object);

            _contentTemplateBuilderMock
                .Setup(it => it.ReplaceKeyWith(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_contentTemplateBuilderMock.Object);

            _contentTemplateBuilderMock
                .Setup(it => it.Build())
                .Returns(Body);
        }
    }
}
