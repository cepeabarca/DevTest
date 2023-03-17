using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DevTest.BackEnd.Data.Models
{
    public class DevTestContext : DbContext
    {
        public DevTestContext(DbContextOptions<DevTestContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>(cat => {
                cat.ToTable("Employee");
                cat.HasKey(p => p.ID);
                cat.Property(p => p.Name).IsRequired().HasMaxLength(100);
                cat.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                cat.Property(p => p.RFC).IsRequired().HasMaxLength(13);
                cat.Property(p => p.BornDate);
                cat.Property(p => p.Status).IsRequired();
            });
        }
    }
}
