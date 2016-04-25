using System;
using System.Net;
using System.Net.Mail;

namespace IIM.Helpers
{
    public class MailService
    {
        private string OriginAddress { get; set; }
        private int Port { get; set; }
        private string Host { get; set; }
        private SmtpClient Client { get; set; }
        private string Password { get; set; }

        public MailService()
        {
            OriginAddress = "donotreply.iim@gmail.com";
            Port = 587;
            Host = "smtp.gmail.com";
            Password = "Pieteriscool";
            InitializeSmtp();

        }

        public MailService(string originAddress, int port, string host, string password)
        {
            OriginAddress = originAddress;
            Port = port;
            Host = host;
            Password = password;
            InitializeSmtp();
        }

        private void InitializeSmtp()
        {
            Client = new SmtpClient
            {
                Host = Host,
                Port = Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(OriginAddress, Password),
                EnableSsl = true
            };
        }

        public void SendMail(string body, string subject, string recipientEmail)
        {
            try
            {
                var mail = new MailMessage(OriginAddress, recipientEmail)
                {
                    Subject = subject,
                    IsBodyHtml = false,
                    Body = body
                };
                Client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }
        }
    }
}