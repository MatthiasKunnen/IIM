using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IIM.Models.DAL
{
    public class MaterialRepository:IMaterialRepository
    {
        private IIMContext context;
        private DbSet<Material> materials;
        public MaterialRepository(IIMContext context)
        {
            this.context = context;
            materials = context.Materials;
        }
        public Material FindByName(string name)
        {
            return materials.SingleOrDefault(m => m.Name == name);
        }
        public IQueryable<Material> FindAll()
        {
            return materials;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        
    }
}