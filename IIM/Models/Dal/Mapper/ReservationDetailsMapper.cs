using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class ReservationDetailsMapper : EntityTypeConfiguration<ReservationDetails>
    {
        public ReservationDetailsMapper()
        {
            ToTable("RESERVATIONDETAIL");
            HasKey(r => r.Id);
            Property(m => m.BroughtBackDate).IsRequired().HasColumnType("TIMESTAMP");

            HasOptional(r => r.MaterialIdentifier).WithMany().Map(r => r.MapKey("MATERIALIDENTIFIER_ID"));
            HasOptional(r => r.Reservation).WithMany().Map(r => r.MapKey("RESERVATION_ID"));
        }
    }
}