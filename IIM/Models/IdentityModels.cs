using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using IIM.Models.DAL;
using IIM.Models.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using IIM.Helpers;

namespace IIM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Faculty { get; set; }
        public Domain.Type Type { get; set; }
        public string Base64Photo { get; set; }
        public bool IsLocal { get; set; }
        public virtual Cart WishList { get; private set; }
        public virtual List<Reservation> Reservations { get; private set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void ClearWishList()
        {
            if (WishList != null)
            {
                IIMContext.Create().Entry(WishList).State = EntityState.Deleted;
            }
        }


        public Cart CreateWishList()
        {
            return (WishList = WishList ?? new Cart());
        }

        public void CreateReservation(DateTime startDate, DateTime endDate, Dictionary<Material,int> requestedMaterials)
        {
            var res = new Reservation(DateTime.Now, startDate, endDate, this);
            foreach(var material in requestedMaterials.Keys)
            {
                res.AddMaterial(material, requestedMaterials[material]);
            }
            Reservations.Add(res);
            MailService.SendMailAsync(res.ReservationBody(), "Uw reservatie werd geregistreerd", Email);
            
        }

        public IEnumerable<MaterialIdentifier> GetPreviousIdentifierRange(DateTime startDate, DateTime enddate, Material material)
        {
            return Reservations.Where(r => r.StartDate < enddate && r.EndDate > startDate).SelectMany(res => res.Details.Where(d => d.MaterialIdentifier.Material.Equals(material)).Select(d => d.MaterialIdentifier));
        }

        public bool AddMaterialToCart(Material material)
        {
            return (WishList ?? (WishList = new Cart())).AddMaterial(material);
        }

        public bool RemoveMaterialFromCart(Material material)
        {
            return WishList?.RemoveMaterial(material) ?? false;
        }

        public Material GetMaterialFromCart(int id)
        {
            return WishList?.GetMaterial(id);

        }
    }
}