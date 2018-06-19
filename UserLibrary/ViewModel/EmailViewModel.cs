using System.Collections.Generic;
using System.Net.Mail;

namespace UserLibrary.ViewModel
{
    class EmailViewModel
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string FromPassword { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }

        public SmtpClient smtp { get; set; }

        public List<string[]> BodyParamaters { get; set; } = new List<string[]>();
        public List<string> Errors { get; set; } = new List<string>();
    }
}
