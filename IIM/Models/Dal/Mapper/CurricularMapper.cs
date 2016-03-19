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
            HasKey(c => c.Name);
        }
    }
}