using System.Collections.Generic;
using System.Text;
using EmailClient.Wrappers;
using MACSkeptic.Commons.Extensions;
using SimplInject;

namespace EmailClient.Builders
{
    [SimplInject]
    public class ContentTemplateBuilder : IContentTemplateBuilder
    {
        private readonly IReadFileWrapper _readFileWrapper;
        private string _templateName;
        private readonly IDictionary<string, string> _valuesToBeReplaced;

        public ContentTemplateBuilder(IReadFileWrapper readFileWrapper)
        {
            _readFileWrapper = readFileWrapper;
            _valuesToBeReplaced = new Dictionary<string, string>();
        }

        public IContentTemplateBuilder UsingTemplate(string templateName)
        {
            _templateName = templateName;
            return this;
        }

        public IContentTemplateBuilder ReplaceKeyWith(string key, string value)
        {
            _valuesToBeReplaced.Add("{" + key + "}", value);
            return this;
        }

        public string Build()
        {
            var template = _readFileWrapper.Read(_templateName, Encoding.UTF8);

            _valuesToBeReplaced
                .Each(it => template = template.Replace(it.Key, it.Value));

            return template;
        }
    }
}
