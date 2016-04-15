using System.Linq;

namespace IIM.Models.Domain
{
    public interface IUserRepository
    {
        ApplicationUser FindByUserName(string name);
        IQueryable<ApplicationUser> FindAll();
        void SaveChanges();
        void ClearWishList(ApplicationUser user);
    }
}
