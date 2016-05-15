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
        public DateTime? BroughtBackDate { get; private set; }

        public DateTime? PickUpDate { get; private set; }

        public virtual Reservation Reservation { get; private set; }

        public virtual MaterialIdentifier MaterialIdentifier { get; private set;}

        public void OverwriteDetail(Reservation r)
        {
            Reservation.RemoveDetail(this);
            Reservation = r;
            r.AddDetail(this);
        }
    }
}