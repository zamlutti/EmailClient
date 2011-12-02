using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using EmailClient.Builders;
using EmailClient.Helpers;
using EmailClient.Wrappers;
using SimplInject;

namespace EmailClient.Factories
{
    [SimplInject]
    public class EmailFactory : IEmailFactory
    {
        private readonly IEmailContentBuilder _contentBuilder;

        private readonly string _templatesPath =
            ConfigurationManager.AppSettings["EmailClient.Templates.Path"];

        public EmailFactory(IEmailContentBuilder contentBuilder)
        {
            _contentBuilder = contentBuilder;
        }

        public EmailFactory()
        {
            var readFileWrapper = new ReadFileWrapper();
            var contentTemplateBuilder = new ContentTemplateBuilder(readFileWrapper);
            _contentBuilder = new EmailContentBuilder(contentTemplateBuilder);
        }

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

        public Email CreateFromTemplateFor(bool isHtml,
                                           string subjectTemplateName,
                                           string bodyTemplateName,
                                           IEnumerable<KeyValuePair<string, string>> valuesToReplaceOnBody,
                                           params string[] to)
        {
            var subjectTemplatePath = Path.Combine(_templatesPath, subjectTemplateName);
            var bodyTemplatePath = Path.Combine(_templatesPath, bodyTemplateName);

            return CreateFor(isHtml,
                             _contentBuilder.BuildSubjectFor(subjectTemplatePath),
                             _contentBuilder.BuildBodyFor(valuesToReplaceOnBody, bodyTemplatePath),
                             to);
        }
    }
}