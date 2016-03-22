using IIM.Models.Domain;
using System.Data.Entity.ModelConfiguration;

namespace IIM.Models.DAL.Mapper
{
    public class TargetGroupMapper : EntityTypeConfiguration<TargetGroup>
    {
        public TargetGroupMapper()
        {
            ToTable("TargetGroup");

            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired();
        }
    }
}