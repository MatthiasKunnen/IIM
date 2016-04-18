using System.Data.Entity.ModelConfiguration;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class ReservationDetailMapper : EntityTypeConfiguration<ReservationDetail>
    {
        public ReservationDetailMapper()
        {
            ToTable("ReservationDetail");

            HasKey(r => r.Id);
            Property(m => m.BroughtBackDate).IsOptional().HasColumnType("datetime");
            Property(m => m.PickUpDate).IsOptional().HasColumnType("datetime");

            HasRequired(r => r.MaterialIdentifier);
            HasRequired(r => r.Reservation);
        }
    }
}