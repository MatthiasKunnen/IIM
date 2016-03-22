using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IIM.Models.DAL.Mapper
{
    public class MaterialMapper : EntityTypeConfiguration<Material>
    {
        public MaterialMapper()
        {
            ToTable("Material");
            HasKey(m => m.Id);
            Property(m => m.ArticleNr).IsRequired();
            Property(m => m.Name).IsRequired();
            Property(m => m.Description).IsOptional();
            Property(m => m.Encoding).IsOptional();
            Property(m => m.Price).IsOptional();

            HasOptional(m => m.Firm).WithMany().Map(m => m.MapKey("FirmId")).WillCascadeOnDelete(false);

            HasMany(m => m.Curriculars).WithMany().Map(m =>
            {
                m.MapLeftKey("MaterialId");
                m.MapRightKey("CurricularId");
                m.ToTable("MaterialCurricular");
            });

            HasMany(m => m.TargetGroups).WithMany().Map(m =>
            {
                m.MapLeftKey("MaterialId");
                m.MapRightKey("TargetgroupId");
                m.ToTable("MaterialTargetgroup");
            });
        }
    }
}