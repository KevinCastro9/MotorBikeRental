using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MotorBikeRental.Models;

namespace MotorBikeRental.Data.Mappings
{
    public class MotorcycleMap : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.ToTable("tbl_motorcycle");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .HasColumnName("id_motorcycle")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Ano)
                .IsRequired()
                .HasColumnName("ano")
                .HasColumnType("INT");

            builder.Property(x => x.Modelo)
                .IsRequired() 
                .HasColumnName("modelo") 
                .HasColumnType("VARCHAR") 
                .HasMaxLength(50);

            builder.Property(x => x.Placa)
                .IsRequired()
                .HasColumnName("placa")
                .HasColumnType("VARCHAR")
                .HasMaxLength(7)
                .IsUnicode();

            builder.Property(x => x.Statulocacao)
                .IsRequired()
                .HasColumnName("status_locacao")
                .HasColumnType("int");
        }
    }
}
