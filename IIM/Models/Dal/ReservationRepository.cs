using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IIM.Models.Domain;

namespace IIM.Models.DAL
{
    public class ReservationRepository : IReservationRepository
    {

        private IIMContext _context;
        private DbSet<Reservation> _reservationSet;
        public ReservationRepository(IIMContext context)
        {
            this._context = context;
            _reservationSet = context.Reservations;
        }

        public IQueryable<Reservation> FindByUser(ApplicationUser user)
        {
            return _reservationSet.Where(r => r.User == user).AsQueryable();
        }

        public Reservation FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Reservation> FindByDate(DateTime Start, DateTime End)
        {
            return _reservationSet.Where(r => r.StartDate <= End && r.EndDate >= Start);
        }

        public List<MaterialIdentifier> GetAvailableIdentifiers(DateTime startDate, DateTime endDate, int count, Material material)
        {
            IEnumerable<MaterialIdentifier> theAvailableIdentifiers = GetAvailablableIdentifiers(startDate,endDate,material);

            if (theAvailableIdentifiers.Count() >= count)
            {
                return theAvailableIdentifiers.Take(count).ToList();
            }
            else
            {
                return theAvailableIdentifiers.ToList();
            }
        }

        public int GetAmountOfAvailableIdentifiers(DateTime startDate, DateTime endDate, int count, Material material)
        {
            return GetAvailablableIdentifiers(startDate,endDate,material).Count();
        }

        private IEnumerable<MaterialIdentifier> GetAvailablableIdentifiers(DateTime startDate, DateTime endDate, Material material)
        {
            IEnumerable<Reservation> theReservations = FindByDate(startDate, endDate);

            IEnumerable<MaterialIdentifier> theUsedIdentfiers = theReservations.SelectMany(r => r.Details.Select(d => d.MaterialIdentifier).ToList().Where(m => m.Material == material));

            IEnumerable<MaterialIdentifier> theAvailableIdentifiers = material.Identifiers.Except(theUsedIdentfiers);

            return theAvailableIdentifiers;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Remove(Reservation reservation)
        {
            _reservationSet.Remove(reservation);
        }
    }
}