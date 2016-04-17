using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class StaffManager : ITypeManager
    {
        public List<ReservationDetail> GetOverridableIdentifiers(List<ReservationDetail> reservationDetails, Material material, DateTime startDateTime, DateTime endDateTime)
        {
            return new List<ReservationDetail>();
        }
    }
}