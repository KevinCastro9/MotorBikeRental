using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MotorBikeRental.Data.Mappings;
using MotorBikeRental.Models;

namespace MotorBikeRental.Data
{
    public class DataContext : DbContext
    {
        
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Deliveryman> Deliverymans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseNpgsql("server=localhost; Port=5432; Database=MotorbikeRentalDb; User Id=postgres; Password=senha;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Passando os mapeamentos que existem 

            
            modelBuilder.ApplyConfiguration(new MotorcycleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new LocationMap());
            modelBuilder.ApplyConfiguration(new DeliverymanMap());

        }
    }
}
