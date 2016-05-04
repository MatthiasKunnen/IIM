using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Microsoft.Ajax.Utilities;

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


        //WIP
        public void AddCart(Cart cart, IReservationRepository reservationRepository)
        {
            List<MaterialIdentifier> identifiers = new List<MaterialIdentifier>();

            foreach (Material m in cart.Materials)
            {
                //identifiers.AddRange(reservationRepository.GetAvailableIdentifiers(this.StartDate,this.EndDate,cart.Materials[m],m));
            }

            AddAllDetails(identifiers.ConvertAll<ReservationDetail>(m => new ReservationDetail(this, m)));

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

        public string ReservationBody()
        {
            return
                string.Format(
                    "Beste {0} {1}\n\nHierbij een bevestiging van uw reservatie.\nOphalen : {2}\nTerugbrengen : {3}\n\nGereserveerde items: {4}\n\nMet vriendelijke groet\nIIM",
                    User.FirstName,
                    User.LastName,
                    StartDate.ToShortDateString(),
                    EndDate.ToShortDateString(),
                    DetailToString());
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
            var everythingHere = true;

            foreach (var detail in Details)
            {
                var previousRes =
                    detail.MaterialIdentifier.ReservationDetails
                        .LastOrDefault(r => (r.Reservation.StartDate < DateTime.Today)&& (r.PickUpDate != new DateTime(01, 01, 01)));

                if (previousRes == null) continue;
                if (previousRes.BroughtBackDate == null)
                {
                    everythingHere = false;
                }
            }

            return everythingHere;

        }
    }
}