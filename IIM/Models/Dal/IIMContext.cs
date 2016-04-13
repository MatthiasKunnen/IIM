using IIM.Models.Domain;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;

namespace IIM.Models.DAL
{
    public class IIMContext : IdentityDbContext<ApplicationUser>
    {
        public IIMContext() : base("IIM")
        {
        }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> UserSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
        public static IIMContext Create()
        {
            return DependencyResolver.Current.GetService<IIMContext>();
        }
    }
}