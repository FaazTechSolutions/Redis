using Cache.Models;
using Microsoft.EntityFrameworkCore;

namespace Cache.Entity
{
    public class CacheContext : DbContext
    {
        public CacheContext(DbContextOptions<CacheContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
