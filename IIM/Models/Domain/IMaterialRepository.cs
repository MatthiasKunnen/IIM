using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIM.Models.Domain
{
    public interface IMaterialRepository
    {
        Material FindByName(string name);
       IQueryable<Material> FindAll();
        void SaveChanges();
    }
}
