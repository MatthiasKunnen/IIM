using System.Data.Entity.ModelConfiguration;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class UserMapper : EntityTypeConfiguration<ApplicationUser>
    {
        public UserMapper()
        {
            ToTable("User");

            Property(u => u.Faculty).IsRequired();
            Property(u => u.FirstName).IsRequired();
            Property(u => u.LastName).IsRequired();
            Property(u => u.Type).IsRequired();

            HasOptional(u => u.WishList).WithRequired().WillCascadeOnDelete(true);
            HasMany(u => u.Reservations).WithRequired(r => r.User);
        }
    }
}