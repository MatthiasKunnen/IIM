using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class StudentManager : ITypeManager
    {
        public List<ReservationDetail> GetOverridableIdentifiers(List<ReservationDetail> reservationDetails, Material material, DateTime startDateTime, DateTime endDateTime)
        {
            throw new System.NotImplementedException();
        }
    }
}