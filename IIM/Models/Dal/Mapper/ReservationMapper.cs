using System.Data.Entity.ModelConfiguration;
using IIM.Models.Domain;

namespace IIM.Models.DAL.Mapper
{
    public class ReservationMapper : EntityTypeConfiguration<Reservation>
    {
        public ReservationMapper()
        {
            ToTable("Reservation");

            HasKey(r => r.Id);

            Property(r => r.CreationDate).IsRequired().HasColumnType("datetime");
            Property(r => r.StartDate).IsRequired().HasColumnType("datetime");
            Property(r => r.EndDate).IsRequired().HasColumnType("datetime");

            HasRequired(r => r.User);
        }
    }
}