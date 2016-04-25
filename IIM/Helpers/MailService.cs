using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EASendMail;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace IIM.Helpers
{
    public class MailService
    {
        private string OriginAddress { get; set; }
        private int Port { get; set; }
        private string Host { get; set; }
        private SmtpClient Client { get; set; }
        private string Password { get; set; }

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

        public async void SendMailAsync(string body, string subject, string recipientEmail)
        {
            var mail = new MailMessage(OriginAddress, recipientEmail)
            {
                Subject = subject,
                IsBodyHtml = false,
                Body = body
            };
            try
            {
                await Client.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
            }


        }


    }
}