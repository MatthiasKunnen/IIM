using System;

namespace IIM.Models.Domain
{
    public class ReservationDetail
    {
        public ReservationDetail()
        {

        }
        public ReservationDetail(Reservation reservation, MaterialIdentifier identifier)
        {
            Reservation = reservation;
            reservation.AddDetail(this);
            MaterialIdentifier = identifier;
        }

        public int Id { get; private set; }
        public DateTime BroughtBackDate { get; private set; }

        public DateTime PickUpDate { get; private set; }

        public Reservation Reservation { get; private set; }

        public MaterialIdentifier MaterialIdentifier { get; private set; }
    }
}