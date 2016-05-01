using System.Data.Entity.ModelConfiguration;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class CartMapper : EntityTypeConfiguration<Cart>
    {
        public CartMapper()
        {
            ToTable("Cart");
            HasKey(c => new { c.Id, c.UserId });

            Property(c => c.CreationDate).IsRequired();

            HasMany(c => c.Materials).WithMany().Map(m =>
            {
                m.ToTable("CartMaterial");
                m.MapLeftKey("CartId", "UserId");
                m.MapRightKey("MaterialId");
            });
        }
    }
}