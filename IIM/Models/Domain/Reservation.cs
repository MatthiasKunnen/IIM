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
            List<MaterialIdentifier> identifiers = material.GetAvailableIdentifiers(StartDate, EndDate).ToList();
            int idCount = identifiers.Count();
            IEnumerable<MaterialIdentifier> previousIdentifiers = User.GetPreviousIdentifierRange(StartDate.Subtract(TimeSpan.FromDays(7)), EndDate.AddDays(7), material);
            if (idCount < count)
            {
                IEnumerable<ReservationDetail> overridableDetails = material.GetReservationRange(StartDate, EndDate).SelectMany(r => r.GetOverridableIdentifiers(material));
                overridableDetails = overridableDetails.OrderByDescending(d => d.Reservation.CreationDate);
                //methode om mail te sturen naar overriden user waiting for mailservice                
                overridableDetails.OrderBy(d => !previousIdentifiers.Contains(d.MaterialIdentifier)).Take(count - idCount).ForEach(d => d.OverwriteDetail(this));
                count -= count - idCount;
            }

            AddAllDetails(identifiers.OrderBy(i => !previousIdentifiers.Contains(i)).Take(count).Select(i => new ReservationDetail(this, i)).ToList());
        }

        public List<ReservationDetail> GetOverridableIdentifiers(Material material)
        {
            return _reservationManager.GetOverridableIdentifiers(Details, material);
        }

        public String DetailToString()
        {
            var details = "";
            Dictionary<Material, int> overview = new Dictionary<Material, int>();
            Details.ForEach(d =>
            {
                if (overview.ContainsKey(d.MaterialIdentifier.Material)) {
                    overview[d.MaterialIdentifier.Material] += 1;
                }
                else {
                    overview[d.MaterialIdentifier.Material] = 1;
                }
            });
            overview.Keys.ForEach(k => details += k.Name + " : " + overview[k] + "\n" );
            return details;
        }


        public string ReservationBody()
        {
            return
                string.Format(
                    "Beste {0} {1}\n\nHierbij een bevestiging van uw reservatie.\nOphalen : {2}\nTerugbrengen : {3}\n\nGereserveerde items: {4}\n\nMet vriendelijke groet\nIIM",
                    User.FirstName,
                    User.LastName,
                    StartDate.ToShortDateString(),
                    EndDate.ToShortDateString(),
                    this.DetailToString());
        }
    }
}