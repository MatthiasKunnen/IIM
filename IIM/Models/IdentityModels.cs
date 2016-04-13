using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using IIM.Models.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IIM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        private Cart _cart;
        private List<Reservation> _reservations;
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Faculty { get; set; }
        public string Type { get; set; }
        public string Base64Photo { get; set; }
        public bool IsLocal { get; set; }
        public Cart WishList
        {
            get {return _cart ?? (_cart = new Cart());}
            private set { _cart = value; }
        }
        public List<Reservation> Reservations
        {
            get { return _reservations ?? (_reservations = new List<Reservation>()); }
            private set { _reservations = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}