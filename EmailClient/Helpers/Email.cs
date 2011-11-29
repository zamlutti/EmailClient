using System.Collections.Generic;
using System.Text;

namespace EmailClient.Helpers
{
    public class Email
    {
        public Email()
        {
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
            Encoding = Encoding.UTF8;
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public Encoding Encoding { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public IEnumerable<string> Bcc { get; set; }
        public bool IsHtml { get; set; }

    }
}