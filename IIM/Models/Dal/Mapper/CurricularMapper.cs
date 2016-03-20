using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IIM.Models.DAL.Mapper
{
    public class CurricularMapper : EntityTypeConfiguration<Curricular>
    {
        public CurricularMapper()
        {
            //Table
            ToTable("Curricular");

            Property(c => c.Id).IsRequired();
            Property(c => c.Name).IsRequired();
            HasKey(c => c.Name);
        }
    }
}