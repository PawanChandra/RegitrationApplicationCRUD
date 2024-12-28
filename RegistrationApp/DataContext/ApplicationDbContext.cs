using Microsoft.EntityFrameworkCore;
using RegistrationApp.Models;

namespace RegistrationApp.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Registration> Registrations { get; set; }
    }
}
