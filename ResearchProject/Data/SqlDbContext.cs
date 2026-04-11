using Microsoft.EntityFrameworkCore;
using ResearchProject.Models;

namespace ResearchProject.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable(nameof(Product));
            modelBuilder.Entity<User>().ToTable(nameof(User));
        }
    }
}
