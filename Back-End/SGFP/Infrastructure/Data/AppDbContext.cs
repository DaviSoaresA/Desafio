using Microsoft.EntityFrameworkCore;
using SGFP.Domain.Entities;

namespace SGFP.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Finance>().HasOne(u => u.User).WithMany(m => m.Finances).HasForeignKey(c => c.UserId);
        }
    }
}
