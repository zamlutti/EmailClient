using System.Collections.Generic;

namespace EmailClient.Builders
{
    public interface IEmailContentBuilder
    {
        string BuildSubject(string subjectTemplatePath);

        string BuildBodyFor(IEnumerable<KeyValuePair<string, string>> valuesToBeReplaced,
                                            string bodyTemplatePath);
    }
}