using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IIM.Models.DAL.Mapper
{
    public class TargetGroupMapper : EntityTypeConfiguration<TargetGroup>
    {
        public TargetGroupMapper()
        {
            //Table
            ToTable("TargetGroup");

           HasKey(t => t.Id);
           Property(t => t.Name).IsRequired();
        }
    }
}