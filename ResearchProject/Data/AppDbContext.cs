using Microsoft.EntityFrameworkCore;
using ResearchProject.Models;

namespace ResearchProject.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}
