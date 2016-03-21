using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class ReservationMapper : EntityTypeConfiguration<Reservation>
    {
        public ReservationMapper()
        {
            ToTable("RESERVATION");

            HasKey(r => r.Id);

            Property(r => r.CreationDate).IsRequired().HasColumnType("TIMESTAMP");
            Property(r => r.StartDate).IsRequired().HasColumnType("TIMESTAMP");
            Property(r => r.EndDate).IsRequired().HasColumnType("TIMESTAMP");

            HasOptional(r => r.User).WithMany().Map(r => r.MapKey("USER_ID"));
        }
    }
}