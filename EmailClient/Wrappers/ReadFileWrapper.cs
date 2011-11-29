using System.IO;
using System.Text;
using SimplInject;

namespace EmailClient.Wrappers
{
    [SimplInject]
    public class ReadFileWrapper : IReadFileWrapper
    {
        public string Read(string filePath, Encoding encoding)
        {
            return File.ReadAllText(filePath, encoding);
        }

        public string Read(string filePath)
        {
            return Read(filePath, Encoding.UTF8);
        }
    }
}
