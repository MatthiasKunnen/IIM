using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIM.Models.Domain
{
    public interface IReservationRepository
    {
        IQueryable<Reservation> FindByUser(ApplicationUser user);
        Reservation FindById(int id);

        List<MaterialIdentifier> GetAvailableIdentifiers(DateTime startDate, DateTime endDate, int count,Material material);
        void SaveChanges();

        void Remove(Reservation reservation);
    }
}
