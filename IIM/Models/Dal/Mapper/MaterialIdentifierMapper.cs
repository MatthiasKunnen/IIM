using System.Data.Entity.ModelConfiguration;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class MaterialIdentifierMapper : EntityTypeConfiguration<MaterialIdentifier>
    {
        public MaterialIdentifierMapper()
        {
            ToTable("MaterialIdentifier");
            HasKey(m => new { m.Id, m.MaterialId });

            Property(m => m.Place).IsRequired();
            Property(m => m.Visibility).IsRequired();

            HasRequired(m => m.Material);
            HasMany(m => m.ReservationDetails).WithRequired(r => r.MaterialIdentifier);
        }
    }
}