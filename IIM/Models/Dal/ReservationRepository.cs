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
        
        public IQueryable<Reservation> FindByUser(User user)
        {
            return _reservationSet.Where(r => r.User == user).AsQueryable();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}