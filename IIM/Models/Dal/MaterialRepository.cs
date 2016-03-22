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
            return _materials.SingleOrDefault(m => m.Name.Equals(name));
        }
        public IQueryable<Material> FindAll()
        {
            return _materials;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}