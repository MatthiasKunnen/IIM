using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace IIM.Models.Domain
{
    public class MailService
    {
        private string OriginAddress { get; set; }
        private int Port { get; set; }
        private string Host { get; set; }
        private SmtpClient Client { get; set; }
        private string Password {  get; set; }

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
            Client = new SmtpClient();
            Client.Port = Port;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.UseDefaultCredentials = false;
            Client.Credentials = new NetworkCredential(OriginAddress, Password);
            Client.Host = Host;

        }


        public bool SendConfirmation(Reservation reservation)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(OriginAddress);
                mail.To.Add(reservation.User.Email);
                mail.Subject = "Bevestiging reservatie.";
                mail.IsBodyHtml = false;
                mail.Body = string.Format("Beste {0} {1}\n\nHierbij een bevestiging van uw reservatie.\nOphalen : {2}\nTerugbrengen : {3}\n\nGereserveerde items: {4}\n\nMet vriendelijke groet\nIIM",
                    reservation.User.FirstName,
                    reservation.User.LastName,
                    reservation.StartDate.ToShortDateString(),
                    reservation.EndDate.ToShortDateString(),
                    reservation.DetailToString());
                Client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


    }
}