using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using IIM.App_Start;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace IIM.Helpers
{
    public static class MailService
    {



        private static SmtpClient InitializeSmtp(string email)
        {
            var smtpCredential = AppSettings.GetSmtpCredential(email);
            return new SmtpClient
            {
                Host = smtpCredential.Host,
                Port = smtpCredential.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(email, smtpCredential.Password),
                EnableSsl = smtpCredential.EnableSSL
            };
        }

        public static async void SendMailAsync(string body, string subject, string recipientEmail, string originAddress = null)
        {
            originAddress = originAddress ?? AppSettings.DefaultEmailOrigin;
            var client = InitializeSmtp(originAddress);
            var mail = new MailMessage(originAddress, recipientEmail)
            {
                Subject = subject,
                IsBodyHtml = false,
                Body = body
            };
            try
            {
                await client.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
            }


        }


    }
}