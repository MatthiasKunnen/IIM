using System.Data.Entity.ModelConfiguration;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("user");
            HasKey(u => u.Id);

            Property(u => u.Email).IsRequired();
            Property(u => u.Faculty).IsRequired();
            Property(u => u.FirstName).IsRequired();
            Property(u => u.LastName).IsRequired();
            Property(u => u.TelNumber);
            Property(u => u.Type).IsRequired();

            HasOptional(u => u.WishList).WithRequired().Map(m => m.MapKey("CartId"));
            HasMany(u => u.Reservations).WithRequired().Map(m=> m.MapKey("UserId"));
        }
    }
}