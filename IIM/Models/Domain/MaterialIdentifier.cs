using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class MaterialIdentifier
    {

        public int Id { get; private set; }

        public string Place { get; private set; }

        public Visibility Visibility { get; private set; }

        public virtual Material Material { get; private set; }

        public virtual List<ReservationDetail> ReservationDetails { get; private set; }

        public boolean IsAvailable(DateTime startDate, DateTime endDate)
        {
            return ReservationDetails.
        } 
    }
}