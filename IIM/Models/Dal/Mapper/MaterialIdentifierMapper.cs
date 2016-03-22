using System.Data.Entity.ModelConfiguration;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class MaterialIdentifierMapper : EntityTypeConfiguration<MaterialIdentifier>
    {
        public MaterialIdentifierMapper()
        {
            ToTable("MaterialIdentifier");
            HasKey(m => m.Id);

            Property(m => m.Place).IsRequired();
            Property(m => m.Visibility).IsRequired();

            HasRequired(m => m.Material).WithMany().Map(m => m.MapKey("InfoId"));
        }
    }
}