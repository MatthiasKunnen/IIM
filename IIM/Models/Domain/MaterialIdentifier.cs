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

        public bool IsAvailable(DateTime startDate, DateTime endDate, Type userType)
        {
            return !ReservationDetails.Any(
                             r =>
                                 r.Reservation.StartDate < endDate && 
                                 r.Reservation.EndDate > startDate &&
                                 r.Reservation.User.Type >= userType); 
        }

        public IEnumerable<ReservationDetail> GetDetailRange(DateTime endDate, DateTime startDate)
        {
            return ReservationDetails.Where(d => d.Reservation.StartDate < endDate && d.Reservation.EndDate > startDate);
        }

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