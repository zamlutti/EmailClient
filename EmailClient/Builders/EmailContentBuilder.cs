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

        public string BuildSubjectFor(string templatePath)
        {
            return _contentTemplateBuilder
                .UsingTemplate(templatePath)
                .Build();
        }

        public string BuildBodyFor(IEnumerable<KeyValuePair<string, string>> valuesToBeReplaced,
                                   string templatePath)
        {
            var templateLoaded = _contentTemplateBuilder
                .UsingTemplate(templatePath);

            foreach (var keyValuePair in valuesToBeReplaced)
            {
                templateLoaded.ReplaceKeyWith(keyValuePair.Key, keyValuePair.Value);
            }

            return templateLoaded.Build();
        }
    }
}