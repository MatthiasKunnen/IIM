using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class CartMapper : EntityTypeConfiguration<Cart>
    {
        public CartMapper()
        {
            ToTable("Cart");
            HasKey(c => c.Id);

            Property(c => c.CreationDate).IsRequired();

            HasMany(c => c.Materials).WithMany().Map(m =>
            {
                m.ToTable("CartMaterial");
                m.MapLeftKey("CartId");
                m.MapRightKey("MaterialId");
            });

        }
    }
}