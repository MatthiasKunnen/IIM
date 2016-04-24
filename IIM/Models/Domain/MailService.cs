using System;
using System.Collections.Generic;
using System.Linq;
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

        public MailService()
        {
            OriginAddress = "donotreply.iim@gmail.com";
            Port = 25;
            Host = "smtp.google.com";
            Client = new SmtpClient();
            Client.Port = Port;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.UseDefaultCredentials = false;
            Client.Host = Host;
        }


        public bool SendConfirmation(Reservation reservation)
        {
            try
            {
                MailMessage mail = new MailMessage(OriginAddress, reservation.User.Email);
                
                mail.Subject = "Bevestiging van uw reservatie.";
                mail.Body = String.Format("Beste {0} {1}%n%nHierbij krijgt u een bevestiging van uw reservatie.%nOphalen : {2}%nTerugbrengen : {3}%n%nGereserveerde items: {4}",
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