using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIM.Models.Domain
{
    interface IUserRepository
    {
        User FindByName(string name);
        IQueryable<User> FindAll();
        void SaveChanges();
    }
}
