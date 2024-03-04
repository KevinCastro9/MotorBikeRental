using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MotorBikeRental.Models;

namespace MotorBikeRental.Data.Mappings
{
    public class DeliverymanMap : IEntityTypeConfiguration<Deliveryman>
    {
        public void Configure(EntityTypeBuilder<Deliveryman> builder)
        {
            builder.ToTable("tbl_deliveryman");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id_deliveryman")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Username)
                .IsRequired()
                .HasColumnName("user_name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(300);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.Cnpj)
                .IsRequired()
                .HasColumnName("cnpj")
                .HasColumnType("VARCHAR")
                .HasMaxLength(18);

            builder.Property(x => x.Dateofbirth)
                .IsRequired()
                .HasColumnName("date_nasc")
                .HasColumnType("DATE");

            builder.Property(x => x.Codcnh)
                .IsRequired()
                .HasColumnName("cod_cnh")
                .HasColumnType("VARCHAR")
                .HasMaxLength(9);

            builder.Property(x => x.Typecnh)
                .IsRequired()
                .HasColumnName("type_nch")
                .HasColumnType("VARCHAR")
                .HasMaxLength(2);

            builder.Property(x => x.Pathcnh)
                .IsRequired()
                .HasColumnName("path_cnh")
                .HasColumnType("VARCHAR")
                .HasMaxLength(500);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasColumnType("INT");
        }
    }
}
