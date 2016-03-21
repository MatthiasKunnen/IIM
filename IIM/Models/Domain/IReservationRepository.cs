using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIM.Models.Domain
{
    interface IReservationRepository
    {
        Reservation FindByName(string name);
        IQueryable<Reservation> FindByUser(User user);
        void SaveChanges();
    }
}
