using System.Collections.Generic;
using EmailClient.Helpers;

namespace EmailClient.Factories
{
    public interface IEmailFactory
    {
        Email CreateFor(bool isHtml, string subject, string body, params string[] to);

        Email CreateFromTemplateFor(bool isHtml,
                                    string subjectTemplateName,
                                    string bodyTemplateName,
                                    IEnumerable<KeyValuePair<string, string>> valuesToReplaceOnBody,
                                    params string[] to);
    }
}