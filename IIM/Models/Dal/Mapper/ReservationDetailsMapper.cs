using System.Data.Entity.ModelConfiguration;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class ReservationDetailsMapper : EntityTypeConfiguration<ReservationDetails>
    {
        public ReservationDetailsMapper()
        {
            ToTable("ReservationDetail");

            HasKey(r => r.Id);
            Property(m => m.BroughtBackDate).IsOptional().HasColumnType("datetime");
            Property(m => m.PickUpDate).IsOptional().HasColumnType("datetime");

            HasRequired(r => r.MaterialIdentifier).WithMany().Map(r => r.MapKey("MaterialIdentifierId"));
            HasRequired(r => r.Reservation);
        }
    }
}