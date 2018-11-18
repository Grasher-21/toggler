using Microsoft.EntityFrameworkCore;

namespace TogglerAPI.Models
{
    public class TogglerContext : DbContext
    {
        public TogglerContext(DbContextOptions<TogglerContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Toggle> Toggles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ToggleServicePermission> ToggleServicePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Service>().Property(x => x.ServiceId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ToggleServicePermission>().HasKey(t => new { t.ToggleId, t.ServiceId });
        }
    }
}
