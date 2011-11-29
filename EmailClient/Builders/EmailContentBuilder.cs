using System.Collections.Generic;
using SimplInject;

namespace EmailClient.Builders
{
    [SimplInject]
    public class EmailContentBuilder : IEmailContentBuilder
    {
        private readonly IContentTemplateBuilder _contentTemplateBuilder;

        public EmailContentBuilder(IContentTemplateBuilder contentTemplateBuilder)
        {
            _contentTemplateBuilder = contentTemplateBuilder;
        }

        public string BuildSubject(string subjectTemplatePath)
        {
            return _contentTemplateBuilder
                .UsingTemplate(subjectTemplatePath)
                .Build();
        }

        public string BuildBodyFor(IEnumerable<KeyValuePair<string, string>> valuesToBeReplaced,
                                   string bodyTemplatePath)
        {
            var templateLoaded = _contentTemplateBuilder
                .UsingTemplate(bodyTemplatePath);

            foreach (var keyValuePair in valuesToBeReplaced)
            {
                templateLoaded.ReplaceKeyWith(keyValuePair.Key, keyValuePair.Value);
            }

            return templateLoaded.Build();
        }
    }
}