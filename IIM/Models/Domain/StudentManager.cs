using System;
using System.Collections.Generic;
using System.Linq;

namespace IIM.Models.Domain
{
    public class StudentManager : ITypeManager
    {
        public List<ReservationDetail> GetOverridableIdentifiers(List<ReservationDetail> reservationDetails, Material material)
        {
            return reservationDetails.Where(d => d.MaterialIdentifier.Material.Equals(material)).ToList();
        }

        public bool IsOverridable(List<ReservationDetail> reservationDetails, DateTime startDate, DateTime endDate)
        {
            return false;
        }
    }
}