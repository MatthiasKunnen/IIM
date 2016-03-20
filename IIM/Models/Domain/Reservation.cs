using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public User User { get; set; }

        public List<ReservationDetails> Details { get; set; }
    }
}