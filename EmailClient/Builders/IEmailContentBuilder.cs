using System.Collections.Generic;

namespace EmailClient.Builders
{
    public interface IEmailContentBuilder
    {
        string BuildSubjectFor(string templatePath);

        string BuildBodyFor(IEnumerable<KeyValuePair<string, string>> valuesToBeReplaced,
                                            string templatePath);
    }
}