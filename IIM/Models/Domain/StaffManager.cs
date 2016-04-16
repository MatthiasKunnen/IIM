using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class StaffManager : ITypeManager
    {
        public List<ReservationDetail> GetOverridableIdentifiers(List<ReservationDetail> reservationDetails)
        {
            return new List<ReservationDetail>();
        }
    }
}