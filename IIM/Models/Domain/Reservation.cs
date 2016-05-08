using System;
using System.Collections.Generic;
using System.Linq;
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
                overridableDetails.OrderBy(d => !previousIdentifiers.Contains(d.MaterialIdentifier)).Take(count - idCount).ForEach(d => d.OverwriteDetail(this));
                count -= count - idCount;
            }

            AddAllDetails(identifiers.OrderBy(i => !previousIdentifiers.Contains(i)).Take(count).Select(i => new ReservationDetail(this, i)).ToList());
        }

        public List<ReservationDetail> GetOverridableIdentifiers(Material material)
        {
            return _reservationManager.GetOverridableIdentifiers(Details, material);
        }

        public string DetailToString()
        {
            var details = "";
            Details.ForEach(i => details += i.MaterialIdentifier.Material.Name + " ");
            return details;
        }
        public bool IsCompleted()
        {
            var completed = true;

            foreach (var r in Details)
            {
                if (!r.BroughtBackDate.HasValue)
                {
                    completed = false;
                }
            }

            return completed;

        }

        public bool IsEverythingHere()
        {

            foreach (var detail in Details)
            {
                if (!detail.MaterialIdentifier.IsHere())
                {
                    return false;
                }
            }

            return true;

        }
        public string ReservationBody => $"Beste {User.FirstName} {User.LastName}\n\nHierbij een bevestiging van uw reservatie.\nOphalen : {StartDate.ToShortDateString()}\nTerugbrengen : {EndDate.ToShortDateString()}\n\nGereserveerde items: \n{string.Join("\n ", Details.GroupBy(rd => rd.MaterialIdentifier.Material).Select(n => $"{n.Key.Name}: {n.Count()}"))}\n\nBedankt voor het gebruikmaken van onze service.";

    }
}