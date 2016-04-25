using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

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
            Port = 25;
            Host = "smtp.google.com";
            Client = new SmtpClient();
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
                Port = Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(OriginAddress, Password),
                Host = Host
            };
        }

        public void SendMail(string body, string subject, string recipientEmail)
        {
            try
            {
                var mail = new MailMessage { From = new MailAddress(OriginAddress) };
                mail.To.Add(recipientEmail);
                mail.Subject = subject;
                mail.IsBodyHtml = false;
                mail.Body = body;
                Client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }
        }
    }
}