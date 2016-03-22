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
            Property(u => u.Faculty);
            Property(u => u.FirstName);
            Property(u => u.LastName);
            Property(u => u.TelNumber);
            Property(u => u.Type).IsRequired();
        }
    }
}