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

            Property(u => u.Email).IsRequired();
            Property(u => u.Faculty).IsRequired();
            Property(u => u.FirstName).IsRequired();
            Property(u => u.LastName).IsRequired();
            Property(u => u.TelNumber);
            Property(u => u.Type).IsRequired();
        }
    }
}