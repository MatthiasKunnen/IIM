using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IIM.Models.DAL
{
    public class MaterialRepository : IMaterialRepository
    {
        private IIMContext _context;
        private DbSet<Material> _materials;
        public MaterialRepository(IIMContext context)
        {
            this._context = context;
            _materials = context.Materials;
        }
        public Material FindByName(string name)
        {
            return _materials.Include(m => m.Curriculars).Include(m => m.Firm).Include(m => m.TargetGroups).SingleOrDefault(m => m.Name.Equals(name));
        }
        public IQueryable<Material> FindAll()
        {
            return _materials;
        }
        public Material FindById(int id)
        {
            return _materials.Include(m => m.Identifiers
            .Select(i => i.ReservationDetails
            .Select(d => d.Reservation.User)))
            .SingleOrDefault(m => m.Id == id);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}