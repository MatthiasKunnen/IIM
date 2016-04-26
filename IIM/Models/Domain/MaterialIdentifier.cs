using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;

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
                                r.Reservation.User.Type < userType); //yes I know this gon be wrong
        }

        public IEnumerable<ReservationDetail> GetDetailRange(DateTime endDate, DateTime startDate)
        {
            return ReservationDetails.Where(d => d.Reservation.StartDate < endDate && d.Reservation.EndDate > startDate);
        }
    }
}