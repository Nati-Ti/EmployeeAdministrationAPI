using Microsoft.EntityFrameworkCore;
using EmployeeAdmin.Persistence.EntityConfigurations;
using EmployeeAdmin.Domain.Model;

namespace EmployeeAdmin.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> EmployeePositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());




        }
    }
}
