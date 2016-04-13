 using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IIM.Models.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly IIMContext _context;
        private readonly IDbSet<ApplicationUser> _userSet;
        public UserRepository(IIMContext context)
        {
            this._context = context;
            _userSet = context.Users;
        }

        public IQueryable<ApplicationUser> FindAll()
        {
            return _userSet;
        }

        public ApplicationUser FindByFirstName(string name)
        {
            return _userSet.First(u => u.FirstName == name);
        }

        public ApplicationUser FindByName(string name)
        {
<<<<<<< HEAD
            return _userSet.First(u => u.UserName == name);
=======
            return _userSet.Where(u => u.LastName == name).First();
        }

        public User FindById(int id)
        {
            return _userSet.Find(id);
>>>>>>> 9f451aff4606cd6edb317b5c6be6deb0b7885fe7
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}