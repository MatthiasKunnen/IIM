using IIM.Models.Domain;
using System.Data.Entity.ModelConfiguration;

namespace IIM.Models.DAL.Mapper
{
    public class CurricularMapper : EntityTypeConfiguration<Curricular>
    {
        public CurricularMapper()
        {
            ToTable("Curricular");
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired();
        }
    }
}