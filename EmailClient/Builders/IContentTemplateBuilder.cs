namespace EmailClient.Builders
{
    public interface IContentTemplateBuilder
    {
        IContentTemplateBuilder UsingTemplate(string templateName);
        IContentTemplateBuilder ReplaceKeyWith(string key, string value);
        string Build();
    }
}