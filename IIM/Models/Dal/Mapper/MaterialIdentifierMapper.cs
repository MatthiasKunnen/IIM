using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class MaterialIdentifierMapper : EntityTypeConfiguration<MaterialIdentifier>
    {
        public MaterialIdentifierMapper()
        {
            ToTable("MaterialIdentifier");
            HasKey(m => m.Id);

            Property(m => m.Place).IsOptional();
            Property(m => m.Visibility).IsOptional();

            HasRequired(m => m.Material).WithMany().Map(m => m.MapKey("InfoId"));
        }
    }
}