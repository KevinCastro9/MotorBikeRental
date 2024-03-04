using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MotorBikeRental.Models;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace MotorBikeRental.Data.Mappings
{
    public class LocationMap : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("tbl_locacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id_locacao")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Startdate)
                .IsRequired()
                .HasColumnName("start_date")
                .HasColumnType("DATE");

            builder.Property(x => x.Enddate)
                .IsRequired()
                .HasColumnName("end_date")
                .HasColumnType("DATE");

            builder.Property(x => x.Valueforecast)
                .IsRequired()
                .HasColumnName("value_forecast")
                .HasColumnType("FLOAT");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("status")
            .HasColumnType("INT");

            builder.Property(x => x.Idmotorcycle)
                .IsRequired()
                .HasColumnName("id_motorcycle")
            .HasColumnType("INT");

            builder.Property(x => x.Iddeliveryman)
                .IsRequired()
                .HasColumnName("id_deliveryman")
            .HasColumnType("INT");
        }
    }
}
