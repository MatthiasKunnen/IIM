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
            //Table
            ToTable("Material");
            HasKey(m => m.Id);
            HasMany(m => m.TargetGroups).WithOptional().Map(m => m.MapKey("Id")).WillCascadeOnDelete(false);
            HasMany(m => m.Curriculars).WithOptional().Map(m => m.MapKey("Id"));
        }
    }
}