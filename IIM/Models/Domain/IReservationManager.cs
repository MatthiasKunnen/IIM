using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public interface IReservationManager
    {
        List<ReservationDetail> GetOverridableIdentifiers(List<ReservationDetail> reservationDetails, Material material);

        bool IsOverridable(List<ReservationDetail> reservationDetails, DateTime startDate, DateTime endDate);
    }
}
