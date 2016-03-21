using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("user");

            Property(u => u.Email).IsOptional();
            Property(u => u.Faculty).IsOptional();
            Property(u => u.FirstName).IsOptional();
            Property(u => u.LastName).IsOptional();
            Property(u => u.TelNumber).IsOptional();
            Property(u => u.Type).IsOptional();

        }
    }
}