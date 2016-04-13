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
        private IIMContext _context;
        private DbSet<User> _userSet;
        public UserRepository(IIMContext context)
        {
            this._context = context;
            _userSet = context.UserSet;
        }

        public IQueryable<User> FindAll()
        {
            return _userSet;
        }

        public User FindByFistName(string name)
        {
            return _userSet.Where(u => u.FirstName == name).First();
        }

        public User FindByName(string name)
        {
            return _userSet.Where(u => u.LastName == name).First();
        }

        public User FindById(int id)
        {
            return _userSet.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}