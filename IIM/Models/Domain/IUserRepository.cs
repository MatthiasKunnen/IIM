using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIM.Models.Domain
{
    public interface IUserRepository
    {
        ApplicationUser FindByName(string name);
        IQueryable<ApplicationUser> FindAll();
        void SaveChanges();
        ApplicationUser GetCurrentUser();
    }
}
