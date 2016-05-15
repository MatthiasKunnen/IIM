using System;
using System.Collections.Generic;
using System.Linq;

namespace IIM.Models.Domain
{
    public class StaffManager : ITypeManager
    {
        public List<ReservationDetail> GetOverridableIdentifiers(List<ReservationDetail> reservationDetails, Material material)
        {
            return new List<ReservationDetail>();
        }

        public bool IsOverridable(List<ReservationDetail> reservationDetails, DateTime startDate, DateTime endDate)
        {
            return reservationDetails.Any(rd => 
                rd.Reservation.StartDate <= startDate 
                    && rd.Reservation.EndDate >= endDate 
                    && rd.Reservation.User.Type == Type.Student);
        }
    }
}