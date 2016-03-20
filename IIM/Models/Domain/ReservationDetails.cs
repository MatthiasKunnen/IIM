using System;

namespace IIM.Models.Domain
{
    public class ReservationDetails
    {
        public DateTime BroughtBackDate { get; set; }

        public DateTime PickUpDate { get; set; }

        public Reservation Reservation { get; set; }

        public MaterialIdentifier MaterialIdentifier { get; set; }
    }
}