using Microsoft.EntityFrameworkCore;
using RegistrationApp.Models;
using RegistrationApp.Models.CoreModels;

namespace RegistrationApp.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<CountryManager> Countries { get; set; }
        public DbSet<StateManager> States { get; set; }
        public DbSet<CityManager> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Country)
                .WithMany()
                .HasForeignKey(r => r.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.State)
                .WithMany()
                .HasForeignKey(r => r.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.City)
                .WithMany()
                .HasForeignKey(r => r.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
