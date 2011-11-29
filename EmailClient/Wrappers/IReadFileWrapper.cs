using System.Text;

namespace EmailClient.Wrappers
{
    public interface IReadFileWrapper
    {
        string Read(string filePath, Encoding encoding);
        string Read(string filePath);
    }
}