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
            return _userSet.Include(u => u.WishList);
        }

        public ApplicationUser FindByFirstName(string name)
        {
            return _userSet.First(u => u.FirstName == name);
        }

        public ApplicationUser FindByName(string name)
        {
            return _userSet.First(u => u.UserName == name);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public ApplicationUser GetCurrentUser()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated
                ? FindAll().First(u => u.UserName.Equals(HttpContext.Current.User.Identity.Name))
                : null;
        }
    }
}