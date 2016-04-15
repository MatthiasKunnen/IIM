using IIM.Models.Domain;
using System.Data.Entity;
using System.Linq;

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

        public ApplicationUser FindByUserName(string name)
        {
            return FindAll().First(u => u.UserName == name);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void ClearWishList(ApplicationUser user)
        {
            _context.Entry(user.WishList).State = EntityState.Deleted;
        }
    }
}