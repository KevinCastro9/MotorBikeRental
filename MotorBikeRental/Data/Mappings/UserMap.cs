using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorBikeRental.Models;

namespace MotorBikeRental.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tbl_user");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .HasColumnName("id_user")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Username)
                .IsRequired()
                .HasColumnName("user_name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(300)
                .IsUnicode();

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasColumnName("role")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasColumnType("int");
        }
    }
}
