using System;
using System.Collections.Generic;
using System.Linq;

namespace IIM.Models.Domain
{
    public class MaterialIdentifier
    {

        public int Id { get; private set; }

        public string Place { get; private set; }

        public Visibility Visibility { get; private set; }

        public virtual Material Material { get; private set; }

        public virtual List<ReservationDetail> ReservationDetails { get; private set; }

        public bool IsHere()
        {
            var here = true;
            var lastRes = ReservationDetails.LastOrDefault(r => r.PickUpDate.HasValue);

            if (lastRes == null) return here;
            if (!lastRes.BroughtBackDate.HasValue)
            {
                here = false;
            }

            return here;
        }
    }
}