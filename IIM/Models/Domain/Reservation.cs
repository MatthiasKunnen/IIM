using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class Reservation
    {
        public Reservation(DateTime creationDate, DateTime startDate, DateTime endDate, User user)
        {
            Details = new List<ReservationDetails>();
            CreationDate = creationDate;
            StartDate = startDate;
            EndDate = endDate;
            User = user;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime StartDate
        {
            get { return StartDate; }
            set
            {
                if (value.Date > DateTime.Today)
                {
                    StartDate = value;
                }
                else
                {
                    throw new ArgumentException("De startdatum van een reservatie moet later zijn dan vanddag.");
                }
            }
        }

        public DateTime EndDate
        {
            get { return EndDate; }
            private set
            {
                if (StartDate.AddDays(7) >= value)
                {
                    EndDate = value;
                }
            }
        }

        public User User { get; private set; }

        public List<ReservationDetails> Details { get; }

        public void AddDetail(ReservationDetails details)
        {
            Details.Add(details);
        }

        public void AddAllDetails(List<ReservationDetails> details)
        {
            Details.AddRange(details);
        }

        public void RemoveDetail(ReservationDetails detail)
        {
            Details.Remove(detail);
        }

        public void RemoveAllDetails(List<ReservationDetails> details)
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

            AddAllDetails(identifiers.ConvertAll<ReservationDetails>(m => new ReservationDetails(this, m)));

        }
    }
}