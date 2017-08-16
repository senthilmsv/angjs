using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HiAsgRAS.Common
{
    public class MailHelper
    {
        private const int Timeout = 180000;
        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _pass;
        private readonly bool _ssl;
        private readonly string _sender;

        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string RecipientCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFile { get; set; }

        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        public MailHelper()
        {
            //MailServer - Represents the SMTP Server
            _host = ConfigurationManager.AppSettings["MailServer"];
            //Port- Represents the port number            
             int.TryParse(ConfigurationManager.AppSettings["Port"], out _port);
            //MailAuthUser and MailAuthPass - Used for Authentication for sending email
            _user = ConfigurationManager.AppSettings["MailAuthUser"];
            _pass = ConfigurationManager.AppSettings["MailAuthPass"];
            _ssl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
            _sender = ConfigurationManager.AppSettings["Sender"];
        }

        public void Send()
        {
            try
            {
                if (string.IsNullOrEmpty(Sender))
                {
                    Sender = _sender;
                }
                // We do not catch the error here... let it pass direct to the caller
                Attachment att = null;
                var message = new MailMessage(Sender, Recipient, Subject, Body) { IsBodyHtml = true };
                if (RecipientCC != null)
                {
                    message.Bcc.Add(RecipientCC);
                }

                SmtpClient smtp = new SmtpClient(_host);
                if (_port > 0)
                {
                    smtp.Port = _port;
                }

                if (!String.IsNullOrEmpty(AttachmentFile))
                {
                    if (File.Exists(AttachmentFile))
                    {
                        att = new Attachment(AttachmentFile);
                        message.Attachments.Add(att);
                    }
                }

                if (!String.IsNullOrEmpty(FileName))
                {
                    Stream stream = new MemoryStream(FileContent);

                    att = new Attachment((Stream)stream, FileName);
                    //att = new Attachment((Stream)stream, FileType);

                    message.Attachments.Add(att);
                }

                if (_user.Length > 0 && _pass.Length > 0)
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_user, _pass);
                    smtp.EnableSsl = _ssl;
                }

                smtp.Send(message);

                if (att != null)
                    att.Dispose();
                message.Dispose();
                smtp.Dispose();
            }

            catch (Exception ex)
            {

            }
        }

      
    }
}
