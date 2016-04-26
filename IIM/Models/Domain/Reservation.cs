using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using WebGrease.Css.Extensions;

namespace IIM.Models.Domain
{
    public class Reservation
    {
        private readonly IReservationManager _reservationManager;
       
        protected Reservation()
        {
        }

        public Reservation(DateTime creationDate, DateTime startDate, DateTime endDate, ApplicationUser user)
        {
            Details = new List<ReservationDetail>();
            CreationDate = creationDate;
            StartDate = startDate;
            EndDate = endDate;
            User = user;
            _reservationManager = TypeManagerFactory.CreateTypeManager(user.Type);
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime StartDate { get; private set; }
    
        public DateTime EndDate { get; private set; }

        public ApplicationUser User { get; private set; }

        public virtual List<ReservationDetail> Details { get; set; }

        public void AddDetail(ReservationDetail detail)
        {
            Details.Add(detail);
        }

        public void AddAllDetails(List<ReservationDetail> details)
        {
            Details.AddRange(details);
        }

        public void RemoveDetail(ReservationDetail detail)
        {
            Details.Remove(detail);
        }

        public void RemoveAllDetails(List<ReservationDetail> details)
        {
            details.ForEach(d => details.Remove(d));
        }

        public void AddMaterial(Material material, int count)
        {
            List<MaterialIdentifier> identifiers = material.GetAvailableIdentifiers(StartDate, EndDate).Take(count).ToList();
            int idCount = identifiers.Count();
            if (idCount == count)
            {
                AddAllDetails(identifiers.Select(i => new ReservationDetail(this, i)).ToList());
            }
            else
            {
                IEnumerable<ReservationDetail> overridableDetails = material.GetReservationRange(StartDate,EndDate).SelectMany(r=> r.GetOverridableIdentifiers(material));
                overridableDetails = overridableDetails.OrderByDescending(d => d.Reservation.CreationDate).Take(count-idCount);
                //methode om mail te sturen naar overriden user waiting for mailservice
                overridableDetails.ForEach(d=> d.OverwriteDetail(this));
            }
        }

        public List<ReservationDetail> GetOverridableIdentifiers(Material material)
        {
            return _reservationManager.GetOverridableIdentifiers(Details, material);
        }

        public String DetailToString()
        {
            var details = "";
            Details.ForEach(i => details += i.MaterialIdentifier.Material.Name + " ");
            return details;
        }
        
        public Boolean sendConfirmation()
        {
            try {
                MailMessage mail = new MailMessage("donotreply.iim@gmail.com", User.Email);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.google.com";
                mail.Subject = "Bevestiging van uw reservatie.";
                mail.Body = String.Format("Beste %s %s%n%nHierbij krijgt u een bevestiging van uw reservatie.%nOphalen : %s%nTerugbrengen : %s%n%nGereserveerde items: %s",
                    User.FirstName,
                    User.LastName,
                    StartDate.ToShortDateString(),
                    EndDate.ToShortDateString(),
                    this.DetailToString());
                client.Send(mail);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
    }
}