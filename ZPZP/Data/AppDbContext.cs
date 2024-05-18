using Microsoft.EntityFrameworkCore;
using ZPZP.Models;

namespace ZPZP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; } // import namespace ctrl + .

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
